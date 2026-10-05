using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractDetailPriceRepository : BaseRepository<EngineeringDBContext, ContractorContractDetailPrice>, IContractorContractDetailPriceRepository
{
    public ContractorContractDetailPriceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetSuggestedServicePriceResponse> GetSuggestedServicePrice(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        List<long>? serviceInfoIds,
        string? filterData,
        DateTime? startDate,
        DateTime? endDate,
        decimal? maxPrice,
        decimal? minPrice,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(c =>
                !c.IsDeleted &&
                !c.ContractorContractDetail.IsDeleted &&
                !c.ContractorContractDetail.ContractorContract.IsDeleted &&
                !c.ContractorContractDetail.ContractorContract.ContractorContractHeader.IsDeleted &&
                (c.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed) &&
                (ids == null || ids.Contains(c.Id)) &&
                (costCenterIds == null || costCenterIds.Contains(c.ContractorContractDetail.ContractorContract.ContractorContractHeader.CostCenterId.Value)) &&
                (contractorIds == null || contractorIds.Contains(c.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId)) &&
                (projectIds == null || projectIds.Contains(c.ContractorContractDetail.ContractorContract.ProjectId.Value)) &&

                (projectOperationIds == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(x =>
                    projectOperationIds.Contains(x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id))) &&

                (serviceInfoIds == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(x =>
                    serviceInfoIds.Contains(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id))) &&

                (startDate == null || c.StartDate >= startDate) &&
                (endDate == null || c.EndDate <= endDate) &&
                (maxPrice == null || c.Price <= maxPrice) &&
                (minPrice == null || c.Price >= minPrice) &&

                (string.IsNullOrWhiteSpace(filterData) || c.ContractorContractDetail.ContractorContractDetailServices.Any(x =>
                    EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())))
            )
            .Select(item => new GetSuggestedServicePriceModel()
            {
                Id = item.Id,
                ContractorContractHeaderId = item.ContractorContractDetail.ContractorContract.ContractorContractHeader.Id,
                ContractorId = item.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId,
                CostCenterId = item.ContractorContractDetail.ContractorContract.ContractorContractHeader.CostCenterId,
                CostCenterName = item.ContractorContractDetail.ContractorContract.ContractorContractHeader.CostCenter.CostCenterName,
                ContractorContractId = item.ContractorContractDetail.ContractorContract.Id,
                ProjectId = item.ContractorContractDetail.ContractorContract.ProjectId,
                ProjectName = item.ContractorContractDetail.ContractorContract.Project.ProjectName,
                ContractorContractType = item.ContractorContractDetail.ContractorContract.ContractorContractType.GetEnumDescription(),
                ContractorContractDetailId = item.ContractorContractDetail.Id,
                StartDate = item.StartDate,
                Names = item.ContractorContractDetail.ContractorContractDetailServices
                    .Select(x => x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName).Distinct().ToList(),
                EndDate = item.EndDate,
                Price = item.Price,
                CurrencyId = item.CurrencyId,
                IsActive = item.IsActive,
                Created = item.Created,
                CreatorId = item.CreatorId,
                UpdaterId = item.UpdaterId,
                Updated = item.Updated,
                UnitOfMeasurementId = item.ContractorContractDetail.ContractorContractDetailServices
                    .FirstOrDefault().ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                Status = item.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status,
            });

        var data = await query.ToListAsync();

        var pricingModel = new PricingModel
        {
            MinPrice = data.Any() ? data.Where(x => x.Price != 0).Min(x => x.Price) : 0,
            MaxPrice = data.Any() ? data.Max(x => x.Price) : 0,
            AveragePrice = data.Any() ? data.Average(x => x.Price) : 0
        };

        query = query.OrderByDescending(c => c.Created);

        var count = await query.CountAsync(ct);
        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        var result = new GetSuggestedServicePriceResponse
        {
            Data = entites,
            OtherData = pricingModel,
            RowCount = count
        };

        return result;
    }

    public async Task<(List<ContractorContractDetailPrice> Data, int RowCount)> GetsFilteredContractorContractDetailOperationPrice(
        long? projectOperationId,
        long? operationInfoId,
        DateTime? startDate,
        DateTime? endDate,
        long? contractorId,
        long? costCenterId,
        long? projectId,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(c => c.ContractorContractDetail.ContractorContract.ContractorContractHeader)
            .Include(c => c.ContractorContractDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ContractorContractDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.ContractorContractDetail)
                .ThenInclude(c => c.ContractorContractDetailPrices)

            .Where(c =>
                (companyId == null || c.ContractorContractDetail.ContractorContract.ContractorContractHeader.CompanyId == companyId) &&
                !c.IsDeleted &&
                (companyId == null || c.ContractorContractDetail.ContractorContract.ContractorContractType.Equals(ContractorContractType.OperationBased)) &&
                (operationInfoId == null || c.ContractorContractDetail.ProjectOperation!.OperationInfo.Id.Equals(operationInfoId)) &&
                (projectOperationId == null || c.ContractorContractDetail.ProjectOperation!.Id.Equals(projectOperationId)) &&
                !c.ContractorContractDetail.ProjectOperation!.IsDeleted &&
                (startDate == null || c.ContractorContractDetail.ContractorContract.StartDate >= startDate) &&
                (endDate == null || c.ContractorContractDetail.ContractorContract.EndDate <= endDate) &&
                (contractorId == null || c.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId.Equals(contractorId)) &&
                (costCenterId == null || c.ContractorContractDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || c.ContractorContractDetail.ProjectOperation.Project.Id.Equals(projectId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ContractorContract.Id.ToString(), filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ProjectOperation!.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ProjectOperation!.OperationInfo.OperationInfoCode, filterData.MakeLikePattern())));

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(c => c.ContractorContractDetail.ContractorContract.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTrackingWithIdentityResolution().AsSplitQuery().ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<GetsContractorContractDetailPriceModel> Data, int RowCount)> GetsContractorContractDetailPrice(
        long contractorContractHedearId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(c =>
                !c.IsDeleted &&
                c.ContractorContractDetail.ContractorContract!.ContractorContractHeader.Id == contractorContractHedearId &&
                (startDate == null || c.ContractorContractDetail.ContractorContract.StartDate >= startDate) &&
                (endDate == null || c.ContractorContractDetail.ContractorContract.EndDate <= endDate) &&
                (string.IsNullOrWhiteSpace(filterData) || c.ContractorContractDetail.ContractorContractDetailServices
                    .Any(x => EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())) ||
                (string.IsNullOrWhiteSpace(filterData) || c.ContractorContractDetail.ContractorContractDetailServices
                    .Any(x => EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern())))))

            .Select(price => new GetsContractorContractDetailPriceModel()
            {
                Id = price.Id,
                ContractorContractDetailId = price.ContractorContractDetailId,
                StartDate = price.StartDate,
                EndDate = price.EndDate,
                Created = price.Created,
                Price = price.Price,
                IsActive = price.IsActive,
                ServiceInfoId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoUnitOfMeasurementId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                OperationInfoName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoName,
                OperationInfoCode = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoCode,
                OperationInfoUnitOfMeasurementId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.UnitOfMeasurementId,
            });


        query = query.OrderByDescending(c => c.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTrackingWithIdentityResolution().AsSplitQuery().ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<ContractorContractDetailPrice> Data, int RowCount)> GetsFilteredContractorContractDetailServicePrice(
        long? projectOperationServiceId,
        long? serviceInfoId,
        DateTime? startDate,
        DateTime? endDate,
        long? contractorId,
        long? costCenterId,
        long? projectId,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(c => c.ContractorContractDetail.ContractorContract.ContractorContractHeader)
            .Include(c => c.ContractorContractDetail)
            .ThenInclude(c => c.ContractorContractDetailServices)
            .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)
            .Include(c => c.ContractorContractDetail)
            .ThenInclude(c => c.ContractorContractDetailServices)
            .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(c => c.ContractorContractDetail)
            .ThenInclude(c => c.ContractorContractDetailServices)
            .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation)
            .Include(c => c.ContractorContractDetail)
            .ThenInclude(c => c.ContractorContractDetailServices)
            .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.ContractorContractDetail)
                .ThenInclude(c => c.ContractorContractDetailPrices).AsQueryable();

        //TODO
        query = query.Where(c =>
                (companyId == null || c.ContractorContractDetail.ContractorContract.ContractorContractHeader.CompanyId == companyId) &&
                !c.IsDeleted &&
                (companyId == null || c.ContractorContractDetail.ContractorContract.ContractorContractHeader.CompanyId == companyId) &&
                c.ContractorContractDetail.ContractorContract.ContractorContractType == ContractorContractType.Service &&
                (serviceInfoId == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo!.Id.Equals(serviceInfoId))) &&
                (projectOperationServiceId == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.Id.Equals(projectOperationServiceId))) &&
                (startDate == null || c.ContractorContractDetail.ContractorContract.StartDate >= startDate) &&
                (endDate == null || c.ContractorContractDetail.ContractorContract.EndDate <= endDate) &&
                (contractorId == null || c.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId.Equals(contractorId)) &&
                (costCenterId == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId))) &&
                !c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.IsDeleted) &&
                (projectId == null || c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId))) &&
                //(string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())) ||
                //string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.ContractorContractDetail.ContractorContract.Id.ToString(), filterData.MakeLikePattern())));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.OrderByDescending(c => c.ContractorContractDetail.ContractorContract.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTrackingWithIdentityResolution().AsSplitQuery().ToListAsync(ct);

        return (entites, count);
    }

}
