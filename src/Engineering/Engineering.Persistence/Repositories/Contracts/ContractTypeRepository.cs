using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractTypeRepository
    : BaseRepository<EngineeringDBContext, ContractTypeEntity>,
        IContractTypeRepository
{
    public ContractTypeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetContractTypeByIdResponse?> GetContractTypeById(
        long contractId,
        long contractTypeId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractTypeId > 0)
            query = query.Where(contractType =>
                contractType.Id == contractTypeId);

        if (contractId > 0)
            query = query.Where(contractType =>
                contractType.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(contractType =>
                contractType.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        return await query
            .Select(contractType => new GetContractTypeByIdResponse
            {
                Id = contractType.Id,
                ContractId = contractType.ContractId,
                Kind = contractType.Kind,
                PricingMethod = contractType.PricingMethod
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GetContractTypeDetailsResponse?> GetContractTypeDetails(
        long contractId,
        long contractTypeId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
                .Include(contractType => contractType.ContractTypeDetails)
                    .ThenInclude(detail => detail.Adjustment)
                .AsNoTracking();

        if (contractTypeId > 0)
            query = query.Where(contractType =>
                contractType.Id == contractTypeId);

        if (contractId > 0)
            query = query.Where(contractType =>
                contractType.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(contractType =>
                contractType.Contract.CompanyId == companyId);

        var contractType = await query.FirstOrDefaultAsync(ct);

        if (contractType is null)
            return null;

        var detailSourceData = await GetDetailSourceData(
            contractType.ContractTypeDetails,
            ct);

        var response = new GetContractTypeDetailsResponse(
            contractType.ContractId,
            contractType.Id,
            contractType.Kind,
            contractType.PricingMethod,
            contractType.ContractTypeDetails
                .Select(detail =>
                    BuildContractTypeDetailModel(
                        detail,
                        detailSourceData))
                .ToList());

        foreach (var detail in response.Items)
        {
            detail.Amount =
                ContractFinancialMath.CalculateContractTypeDetailAmount(
                    response.PricingMethod,
                    detail.Quantity,
                    detail.UnitPrice,
                    detail.FixedAmount,
                    detail.Duration);
        }

        return response;
    }

    private async Task<ContractTypeDetailSourceData>
        GetDetailSourceData(
            IReadOnlyCollection<ContractTypeDetailEntity> details,
            CT ct)
    {
        var productIds = details
            .Where(detail => detail.ConsumableVolumeProductId.HasValue)
            .Select(detail => detail.ConsumableVolumeProductId!.Value)
            .Distinct()
            .ToList();

        var serviceIds = details
            .Where(detail =>
                detail.ProjectOperationDetailContractorServiceId.HasValue)
            .Select(detail =>
                detail.ProjectOperationDetailContractorServiceId!.Value)
            .Distinct()
            .ToList();

        var operationDetailIds = details
            .Where(detail => detail.ProjectOperationDetailId.HasValue)
            .Select(detail => detail.ProjectOperationDetailId!.Value)
            .Distinct()
            .ToList();

        var products = await GetProducts(productIds, ct);
        var services = await GetServices(serviceIds, ct);
        var operationDetails = await GetOperationDetails(
            operationDetailIds,
            ct);

        var measureUnits = await GetMeasureUnits(
            details
                .Where(detail => detail.UnitOfMeasurementId.HasValue)
                .Select(detail => detail.UnitOfMeasurementId!.Value)
                .Distinct()
                .ToList(),
            ct);

        return new ContractTypeDetailSourceData(
            products,
            services,
            operationDetails,
            measureUnits);
    }

    private async Task<Dictionary<long, ContractTypeDetailSourceInfo>>
        GetProducts(
            IReadOnlyCollection<long> productIds,
            CT ct)
    {
        if (productIds.Count == 0)
            return [];

        var products = DbContext
            .Set<ConsumableVolumeProduct>()
            .AsQueryable()
            .AsNoTracking();

        var groups = DbContext
            .Set<ViewGroup>()
            .AsQueryable()
            .AsNoTracking();

        var values = await (
            from product in products
            join productGroup in groups
                on product.ProductGroupId equals productGroup.Id
            where productIds.Contains(product.Id)
            select new
            {
                product.Id,
                Info = new ContractTypeDetailSourceInfo(
                    productGroup.Name,
                    productGroup.Code)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private async Task<Dictionary<long, ContractTypeDetailSourceInfo>>
        GetServices(
            IReadOnlyCollection<long> serviceIds,
            CT ct)
    {
        if (serviceIds.Count == 0)
            return [];

        var query = DbContext
            .Set<ProjectOperationDetailContractorService>()
            .AsQueryable()
            .AsNoTracking();

        var values = await query
            .Where(service => serviceIds.Contains(service.Id))
            .Select(service => new
            {
                service.Id,
                Info = new ContractTypeDetailSourceInfo(
                    service.OperationInfoService!
                        .ServiceInfo.ServiceInfoName,
                    service.OperationInfoService
                        .ServiceInfo.ServiceInfoCode)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private async Task<Dictionary<long, ContractTypeDetailSourceInfo>>
        GetOperationDetails(
            IReadOnlyCollection<long> operationDetailIds,
            CT ct)
    {
        if (operationDetailIds.Count == 0)
            return [];

        var query = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        var values = await query
            .Where(detail => operationDetailIds.Contains(detail.Id))
            .Select(detail => new
            {
                detail.Id,
                Info = new ContractTypeDetailSourceInfo(
                    detail.Description ?? detail.Code,
                    detail.Code)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private async Task<Dictionary<long, string>>
        GetMeasureUnits(
            IReadOnlyCollection<long> measureUnitIds,
            CT ct)
    {
        if (measureUnitIds.Count == 0)
            return [];

        var query = DbContext
            .Set<MeasureUnit>()
            .AsQueryable()
            .AsNoTracking();

        var values = await query
            .Where(unit => measureUnitIds.Contains(unit.Id))
            .Select(unit => new
            {
                unit.Id,
                unit.Name
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Name);
    }

    private static GetContractTypeDetailsModel
        BuildContractTypeDetailModel(
            ContractTypeDetailEntity detail,
            ContractTypeDetailSourceData sourceData)
    {
        var sourceId =
            detail.ConsumableVolumeProductId ??
            detail.ProjectOperationDetailId ??
            detail.ProjectOperationDetailContractorServiceId ??
            0;

        var source = detail.ConsumableVolumeProductId.HasValue
            ? sourceData.Products.GetValueOrDefault(
                detail.ConsumableVolumeProductId.Value)
            : detail.ProjectOperationDetailContractorServiceId.HasValue
                ? sourceData.Services.GetValueOrDefault(
                    detail.ProjectOperationDetailContractorServiceId.Value)
                : sourceData.OperationDetails.GetValueOrDefault(
                    detail.ProjectOperationDetailId ?? 0);

        return new GetContractTypeDetailsModel
        {
            Id = detail.Id,
            SourceId = sourceId,
            SourceTitle = source?.Title ?? string.Empty,
            SourceCode = source?.Code,
            Quantity = detail.Quantity,
            UnitOfMeasurementId = detail.UnitOfMeasurementId,
            UnitOfMeasurementTitle = detail.UnitOfMeasurementId.HasValue
                ? sourceData.MeasureUnits.GetValueOrDefault(
                    detail.UnitOfMeasurementId.Value)
                : null,
            UnitPrice = detail.UnitPrice,
            FixedAmount = detail.FixedAmount,
            TechnicalSpecifications =
                detail.TechnicalSpecifications,
            ExpectedDeliverables =
                detail.ExpectedDeliverables,
            Duration = detail.Duration,
            DurationUnit = detail.DurationUnit,
            IsSubjectToAdjustment =
                detail.IsSubjectToAdjustment,
            AdjustmentType = detail.Adjustment?.Type
        };
    }
}
