using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Application.Services.Contracts.Models.ContractTypeDetails;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractTypeDetailRepository
    : BaseRepository<EngineeringDBContext, ContractTypeDetailEntity>,
        IContractTypeDetailRepository
{
    public ContractTypeDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetContractTypeDetailByIdResponse?> GetContractTypeDetailById(
        long contractId,
        long contractTypeId,
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(detail => detail.ContractType)
                .ThenInclude(contractType => contractType.Contract)
            .Include(detail => detail.Adjustment)
                .ThenInclude(adjustment => adjustment.PriceIndex)
                    .ThenInclude(priceIndex =>
                        priceIndex.ContractAdjustmentReference)
            .AsNoTracking();

        if (id > 0)
            query = query.Where(detail => detail.Id == id);

        if (contractTypeId > 0)
            query = query.Where(detail =>
                detail.ContractTypeId == contractTypeId);

        if (contractId > 0)
            query = query.Where(detail =>
                detail.ContractType.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(detail =>
                detail.ContractType.Contract.CompanyId == companyId);

        var detail = await query.FirstOrDefaultAsync(ct);

        if (detail is null)
            return null;

        var source = await GetDetailSource(
            detail,
            ct);

        var currencyTitle = await GetCurrencyTitle(
            detail.Adjustment?.CurrencyId,
            ct);

        return new GetContractTypeDetailByIdResponse
        {
            Id = detail.Id,
            ContractId = detail.ContractType.ContractId,
            ContractTypeId = detail.ContractTypeId,
            SourceId = GetSourceId(detail),
            SourceTitle = source.Title,
            SourceCode = source.Code,
            Kind = detail.ContractType.Kind,
            PricingMethod = detail.ContractType.PricingMethod,
            Quantity = detail.Quantity,
            UnitOfMeasurementId = detail.UnitOfMeasurementId,
            UnitOfMeasurementTitle = await GetMeasureUnitTitle(
                detail.UnitOfMeasurementId,
                ct),
            UnitPrice = detail.UnitPrice,
            FixedAmount = detail.FixedAmount,
            TechnicalSpecifications = detail.TechnicalSpecifications,
            ExpectedDeliverables = detail.ExpectedDeliverables,
            Duration = detail.Duration,
            DurationUnit = detail.DurationUnit,
            IsSubjectToAdjustment = detail.IsSubjectToAdjustment,
            Adjustment = BuildAdjustmentResponse(
                detail.Adjustment,
                currencyTitle)
        };
    }

    private async Task<(string Title, string? Code)> GetDetailSource(
        ContractTypeDetailEntity detail,
        CT ct)
    {
        if (detail.ConsumableVolumeProductId.HasValue)
        {
            var products = DbContext
                .Set<ConsumableVolumeProduct>()
                .AsQueryable()
                .AsNoTracking();

            var groups = DbContext
                .Set<ViewGroup>()
                .AsQueryable()
                .AsNoTracking();

            var source = await (
                from product in products
                join productGroup in groups
                    on product.ProductGroupId equals productGroup.Id
                where product.Id ==
                    detail.ConsumableVolumeProductId.Value
                select new
                {
                    productGroup.Name,
                    productGroup.Code
                })
                .FirstOrDefaultAsync(ct);

            return (
                source?.Name ?? string.Empty,
                source?.Code);
        }

        if (detail.ProjectOperationDetailContractorServiceId.HasValue)
        {
            var services = DbContext
                .Set<ProjectOperationDetailContractorService>()
                .AsQueryable()
                .AsNoTracking();

            var source = await services
                .Where(service =>
                    service.Id ==
                    detail.ProjectOperationDetailContractorServiceId.Value)
                .Select(service => new
                {
                    Title = service.OperationInfoService!
                        .ServiceInfo.ServiceInfoName,
                    Code = service.OperationInfoService!
                        .ServiceInfo.ServiceInfoCode
                })
                .FirstOrDefaultAsync(ct);

            return (
                source?.Title ?? string.Empty,
                source?.Code);
        }

        if (detail.ProjectOperationDetailId.HasValue)
        {
            var operationDetails = DbContext
                .Set<ProjectOperationDetail>()
                .AsQueryable()
                .AsNoTracking();

            var source = await operationDetails
                .Where(operationDetail =>
                    operationDetail.Id ==
                    detail.ProjectOperationDetailId.Value)
                .Select(operationDetail => new
                {
                    Title = operationDetail.Description ??
                            operationDetail.Code,
                    Code = operationDetail.Code
                })
                .FirstOrDefaultAsync(ct);

            return (
                source?.Title ?? string.Empty,
                source?.Code);
        }

        return (string.Empty, null);
    }

    private async Task<string?> GetMeasureUnitTitle(
        long? measureUnitId,
        CT ct)
    {
        if (!measureUnitId.HasValue)
            return null;

        var query = DbContext
            .Set<MeasureUnit>()
            .AsQueryable()
            .AsNoTracking();

        return await query
            .Where(unit => unit.Id == measureUnitId.Value)
            .Select(unit => unit.Name)
            .FirstOrDefaultAsync(ct);
    }

    private async Task<string?> GetCurrencyTitle(
        long? currencyId,
        CT ct)
    {
        if (!currencyId.HasValue)
            return null;

        var query = DbContext
            .Set<ViewCurrency>()
            .AsQueryable()
            .AsNoTracking();

        return await query
            .Where(currency => currency.Id == currencyId.Value)
            .Select(currency => currency.Name)
            .FirstOrDefaultAsync(ct);
    }

    private static long GetSourceId(
        ContractTypeDetailEntity detail)
    {
        if (detail.ConsumableVolumeProductId.HasValue)
            return detail.ConsumableVolumeProductId.Value;

        if (detail.ProjectOperationDetailId.HasValue)
            return detail.ProjectOperationDetailId.Value;

        return detail.ProjectOperationDetailContractorServiceId ?? 0;
    }

    private static GetContractTypeDetailAdjustmentResponse?
        BuildAdjustmentResponse(
            ContractTypeDetailAdjustment? adjustment,
            string? currencyTitle)
    {
        if (adjustment is null)
            return null;

        var response = new GetContractTypeDetailAdjustmentResponse
        {
            Id = adjustment.Id,
            Type = adjustment.Type,
            BaseYear = adjustment.PriceIndexBaseYear,
            BasePeriod = adjustment.PriceIndexBasePeriod,
            Reference =
                adjustment.Type ==
                ContractTypeDetailAdjustmentType.PriceIndex
                    ? adjustment.PriceIndex?
                        .ContractAdjustmentReference?.FaTitle
                    : adjustment.OtherReference,
            Index =
                adjustment.Type ==
                ContractTypeDetailAdjustmentType.PriceIndex
                    ? adjustment.PriceIndex?.FaTitle
                    : adjustment.OtherIndex,
            PriceIndexReferenceId =
                adjustment.PriceIndex?.ContractAdjustmentReferenceId,
            PriceIndexReferenceFaTitle =
                adjustment.PriceIndex?
                    .ContractAdjustmentReference?.FaTitle,
            PriceIndexReferenceEnTitle =
                adjustment.PriceIndex?
                    .ContractAdjustmentReference?.EnTitle,
            PriceIndexId = adjustment.PriceIndexId,
            PriceIndexFaTitle =
                adjustment.PriceIndex?.FaTitle,
            PriceIndexEnTitle =
                adjustment.PriceIndex?.EnTitle,
            BaseDate = adjustment.CurrencyBaseDate,
            BaseRate = adjustment.CurrencyBaseRate,
            CurrencyId = adjustment.CurrencyId,
            CurrencyTitle = currencyTitle,
            ReferenceType = adjustment.CurrencyReferenceType,
            CustomReference = adjustment.CurrencyCustomReference,
            Basis = adjustment.OtherBasis,
            Description = adjustment.Description
        };

        return response;
    }

    public async Task<ContractTypeDetailSourceModel?>
        GetProcurementContractTypeDetailSource(
            long sourceId,
            long projectId,
            long? excludedContractTypeDetailId,
            long companyId,
            CT ct)
    {
        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var products = DbContext
            .Set<ConsumableVolumeProduct>()
            .AsQueryable()
            .AsNoTracking();

        var groups = DbContext
            .Set<ViewGroup>()
            .AsQueryable()
            .AsNoTracking();

        var query =
            from product in products
            join productGroup in groups
                on product.ProductGroupId equals productGroup.Id
            where product.Id == sourceId
            select new
            {
                product,
                productGroup
            };

        query = query.Where(value =>
            value.product.VolumeProductType ==
            VolumeProductType.ProductGroup);

        query = query.Where(value =>
            value.product.ProjectOperationDetail
                .ProjectOperation.ProjectId == projectId);

        if (companyId > 0)
        {
            query = query.Where(value =>
                value.product.ProjectOperationDetail
                    .ProjectOperation.Project.CompanyId == companyId);
        }

        var resultQuery = query.Select(value => new ContractTypeDetailSourceModel(
            value.product.Id,
            value.product.FinalValue -
            (allocations
                .Where(detail =>
                    detail.ConsumableVolumeProductId ==
                    value.product.Id &&
                    (!excludedContractTypeDetailId.HasValue ||
                     detail.ContractTypeDetailId !=
                     excludedContractTypeDetailId.Value))
                .Sum(detail =>
                    (decimal?)detail.EffectiveQuantity) ?? 0m),
            value.productGroup.MeasureUnitId,
            null,
            null));

        return await resultQuery.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractTypeDetailSourceModel?>
        GetConstructionContractTypeDetailSource(
            long sourceId,
            long projectId,
            long contractPartyId,
            long? excludedContractTypeDetailId,
            long companyId,
            CT ct)
    {
        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var sourceAllocations =
            GetEffectiveSourceBasedConstructionAllocations();

        var query = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        if (sourceId > 0)
            query = query.Where(source => source.Id == sourceId);

        if (projectId > 0)
        {
            query = query.Where(source =>
                source.ProjectOperation.ProjectId == projectId);
        }

        if (companyId > 0)
        {
            query = query.Where(source =>
                source.ProjectOperation.Project.CompanyId ==
                companyId);
        }

        query = query.Where(source =>
            !source.ProjectOperationDetailContractorServices.Any(service =>
                service.Type ==
                PODContractorServiceType.ServiceBased &&
                service.ContractorId.HasValue &&
                service.ContractorId.Value != contractPartyId));

        var values = await query
            .Select(source => new
            {
                source.Id,
                AvailableAmount =
                    source.FinalAmount -
                    (
                        source.ProjectOperationDetailDeductions
                            .Sum(deduction =>
                                (decimal?)(
                                    deduction.Length *
                                    deduction.Width *
                                    deduction.Height *
                                    deduction.Weight *
                                    deduction.Number)) ?? 0m
                    ) -
                    (
                        source.ProjectOperationDetailContractorServices
                            .Where(service =>
                                service.Type ==
                                PODContractorServiceType.OperationBased)
                            .Sum(service =>
                                (decimal?)service.Volume) ?? 0m
                    ) -
                    (
                        allocations
                            .Where(detail =>
                                detail.ProjectOperationDetailId ==
                                source.Id &&
                                (!excludedContractTypeDetailId.HasValue ||
                                 detail.ContractTypeDetailId !=
                                 excludedContractTypeDetailId.Value))
                            .Sum(detail =>
                                (decimal?)detail.EffectiveQuantity) ?? 0m
                    ) -
                    (
                        sourceAllocations
                            .Where(item =>
                                item.ProjectOperationDetailId ==
                                source.Id)
                            .Sum(item =>
                                (decimal?)item.EffectiveQuantity) ?? 0m
                    ),
                UnitOfMeasurementId =
                    source.ProjectOperation.UnitOfMeasurementId,
                EstimatedUnitPrice =
                    source.ProjectOperation.ChangedPrice *
                    source.ProjectOperation.IncreaseRate
            })
            .FirstOrDefaultAsync(ct);

        if (values is null)
            return null;

        return new ContractTypeDetailSourceModel(
            values.Id,
            values.AvailableAmount,
            values.UnitOfMeasurementId,
            null,
            values.EstimatedUnitPrice);
    }

    public async Task<ContractTypeDetailSourceModel?>
        GetServiceContractTypeDetailSource(
            long sourceId,
            long projectId,
            long? excludedContractTypeDetailId,
            long companyId,
            CT ct)
    {
        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var query = DbContext
            .Set<ProjectOperationDetailContractorService>()
            .AsQueryable()
            .AsNoTracking();

        if (sourceId > 0)
            query = query.Where(service => service.Id == sourceId);

        query = query.Where(service =>
            service.Type == PODContractorServiceType.ServiceBased &&
            service.OperationInfoServiceId.HasValue &&
            service.ProjectOperationDetail.ProjectOperation.ProjectId ==
            projectId);

        if (companyId > 0)
        {
            query = query.Where(service =>
                service.ProjectOperationDetail.ProjectOperation.Project.CompanyId ==
                companyId);
        }

        return await query
            .Select(service => new ContractTypeDetailSourceModel(
                service.Id,
                service.Volume -
                allocations
                    .Where(detail =>
                        detail.ProjectOperationDetailContractorServiceId ==
                        service.Id &&
                        (!excludedContractTypeDetailId.HasValue ||
                         detail.ContractTypeDetailId !=
                         excludedContractTypeDetailId.Value))
                    .Sum(detail =>
                        (decimal?)detail.EffectiveQuantity) ?? 0m,
                null,
                service.ContractorId,
                null))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GetAvailableContractTypeDetailSourcesResponse?>
        GetAvailableContractTypeDetailSources(
            long contractId,
            long contractTypeId,
            long? projectOperationId,
            string? filterData,
            long companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var contractType = await GetContractType(
            contractId,
            contractTypeId,
            companyId,
            ct);

        if (contractType is null)
            return null;

        IQueryable<GetAvailableContractTypeDetailSourcesModel>? query = null;

        if (contractType.Kind == ContractTypeKind.Procurement)
        {
            query = GetAvailableProcurementSources(
                contractType.Id,
                contractType.ProjectId,
                projectOperationId,
                filterData);
        }
        else if (contractType.Kind == ContractTypeKind.Construction)
        {
            query = GetAvailableConstructionSources(
                contractType.Id,
                contractType.ProjectId,
                contractType.ContractPartyId,
                projectOperationId,
                filterData);
        }
        else if (contractType.Kind is
                 ContractTypeKind.Engineering or
                 ContractTypeKind.Services)
        {
            query = GetAvailableServiceSources(
                contractType.Id,
                contractType.ProjectId,
                contractType.ContractPartyId,
                projectOperationId,
                filterData);
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }

        var rowCount = await query.CountAsync(ct);

        query = query.OrderByDescending(source => source.SourceId);

        if (orderBy is { Length: > 0 })
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return new GetAvailableContractTypeDetailSourcesResponse(
            contractType.ContractId,
            contractType.Id,
            contractType.Kind,
            contractType.PricingMethod,
            data,
            rowCount);
    }

    private async Task<ContractTypeSummary?>
        GetContractType(
            long contractId,
            long contractTypeId,
            long companyId,
            CT ct)
    {
        var query = DbContext
            .Set<ContractTypeEntity>()
            .AsQueryable()
            .AsNoTracking();

        if (contractTypeId > 0)
            query = query.Where(type => type.Id == contractTypeId);

        if (contractId > 0)
            query = query.Where(type => type.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(type =>
                type.Contract.CompanyId == companyId);

        return await query
            .Select(type => new ContractTypeSummary(
                type.Id,
                type.ContractId,
                type.Kind,
                type.PricingMethod,
                type.Contract.ProjectId,
                type.Contract.ContractPartyId))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ContractChangeDetailResolutionContextModel>>
        GetContractChangeDetailResolutionContexts(
            long contractId,
            long? excludedContractChangeId,
            IReadOnlyCollection<long> contractTypeDetailIds,
            long companyId,
            CT ct)
    {
        if (contractTypeDetailIds.Count == 0)
            return [];

        var query = DbSet.AsQueryable();

        if (contractId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.Contract.CompanyId ==
                companyId);
        }

        query = query.Where(detail =>
            contractTypeDetailIds.Contains(detail.Id));

        query = query.AsNoTracking();

        var details = await query
            .Select(detail =>
                new ContractChangeDetailContextRow(
                    detail.Id,
                    detail.ContractTypeId,
                    detail.ContractType.Kind,
                    detail.ContractType.PricingMethod,
                    detail.ConsumableVolumeProductId ??
                    detail.ProjectOperationDetailId ??
                    detail.ProjectOperationDetailContractorServiceId ??
                    0,
                    detail.Quantity,
                    detail.FixedAmount,
                    detail.UnitOfMeasurementId,
                    detail.UnitPrice))
            .ToListAsync(ct);

        if (details.Count == 0)
            return [];

        var itemsQuery = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable();

        itemsQuery = itemsQuery.Where(item =>
            item.ContractTypeDetailId.HasValue &&
            contractTypeDetailIds.Contains(
                item.ContractTypeDetailId.Value));

        if (contractId > 0)
        {
            itemsQuery = itemsQuery.Where(item =>
                item.ContractChange.ContractId == contractId);
        }

        if (companyId > 0)
        {
            itemsQuery = itemsQuery.Where(item =>
                item.ContractChange.Contract.CompanyId ==
                companyId);
        }

        itemsQuery = itemsQuery.AsNoTracking();

        var items = await itemsQuery
            .Select(item =>
                new ContractChangeDetailItemRow(
                    item.ContractTypeDetailId!.Value,
                    item.ContractChangeId,
                    item.ContractChange.Date,
                    item.NewValue))
            .ToListAsync(ct);

        var result =
            new List<ContractChangeDetailResolutionContextModel>(
                details.Count);

        foreach (var detail in details)
        {
            var priorValue = items
                .Where(item =>
                    item.ContractTypeDetailId == detail.Id &&
                    (!excludedContractChangeId.HasValue ||
                     item.ContractChangeId !=
                     excludedContractChangeId.Value))
                .OrderByDescending(item => item.Date)
                .ThenByDescending(item => item.ContractChangeId)
                .Select(item => (decimal?)item.NewValue)
                .FirstOrDefault();

            var currentValue = items
                .Where(item =>
                    item.ContractTypeDetailId == detail.Id)
                .OrderByDescending(item => item.Date)
                .ThenByDescending(item => item.ContractChangeId)
                .Select(item => (decimal?)item.NewValue)
                .FirstOrDefault();

            result.Add(
                new ContractChangeDetailResolutionContextModel(
                    detail.Id,
                    detail.ContractTypeId,
                    detail.Kind,
                    detail.PricingMethod,
                    detail.SourceId,
                    detail.Quantity,
                    detail.FixedAmount,
                    detail.UnitOfMeasurementId,
                    detail.UnitPrice,
                    priorValue,
                    currentValue));
        }

        return result;
    }

    public async Task<List<long>> GetBaselineConstructionSourceIds(
        long contractId,
        IReadOnlyCollection<long> projectOperationDetailIds,
        long companyId,
        CT ct)
    {
        if (projectOperationDetailIds.Count == 0)
            return [];

        var query = DbSet.AsQueryable();

        if (contractId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.Contract.CompanyId == companyId);
        }

        query = query.Where(detail =>
            detail.ProjectOperationDetailId.HasValue &&
            projectOperationDetailIds.Contains(
                detail.ProjectOperationDetailId.Value));

        query = query.AsNoTracking();

        return await query
            .Select(detail => detail.ProjectOperationDetailId!.Value)
            .Distinct()
            .ToListAsync(ct);
    }

    private IQueryable<EffectiveContractTypeDetailAllocation>
        GetEffectiveContractTypeDetailAllocations()
    {
        var items = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable()
            .AsNoTracking();

        var latestItems = items.Where(item =>
            item.ContractTypeDetailId.HasValue);

        latestItems = latestItems.Where(item =>
            !items.Any(later =>
                later.ContractTypeDetailId ==
                item.ContractTypeDetailId &&
                (
                    later.ContractChange.Date >
                    item.ContractChange.Date ||
                    (
                        later.ContractChange.Date ==
                        item.ContractChange.Date &&
                        later.ContractChangeId >
                        item.ContractChangeId
                    ))));

        var details = DbSet.AsQueryable().AsNoTracking();

        var query =
            from detail in details
            join latestItem in latestItems
                on detail.Id equals latestItem.ContractTypeDetailId
                into latestItemGroup
            from latestItem in latestItemGroup.DefaultIfEmpty()
            select new EffectiveContractTypeDetailAllocation
            {
                ContractTypeDetailId = detail.Id,
                ConsumableVolumeProductId =
                    detail.ConsumableVolumeProductId,
                ProjectOperationDetailId =
                    detail.ProjectOperationDetailId,
                ProjectOperationDetailContractorServiceId =
                    detail.ProjectOperationDetailContractorServiceId,
                EffectiveQuantity =
                    detail.ContractType.PricingMethod ==
                    PricingMethod.LumpSum
                        ? detail.Quantity
                        : latestItem != null
                            ? latestItem.NewValue
                            : detail.Quantity
            };

        return query;
    }

    private IQueryable<EffectiveSourceBasedConstructionAllocation>
        GetEffectiveSourceBasedConstructionAllocations()
    {
        var items = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable()
            .AsNoTracking();

        var query = items.Where(item =>
            item.ContractTypeDetailId == null &&
            item.ContractTypeId.HasValue &&
            item.ProjectOperationDetailId.HasValue);

        query = query.Where(item =>
            !items.Any(later =>
                later.ContractTypeDetailId == null &&
                later.ContractChange.ContractId ==
                    item.ContractChange.ContractId &&
                later.ContractTypeId == item.ContractTypeId &&
                later.ProjectOperationDetailId ==
                    item.ProjectOperationDetailId &&
                (
                    later.ContractChange.Date >
                        item.ContractChange.Date ||
                    (
                        later.ContractChange.Date ==
                        item.ContractChange.Date &&
                        later.ContractChangeId >
                        item.ContractChangeId
                    ))));

        return query.Select(item =>
            new EffectiveSourceBasedConstructionAllocation
            {
                ContractId = item.ContractChange.ContractId,
                ContractTypeId = item.ContractTypeId!.Value,
                ProjectOperationDetailId =
                    item.ProjectOperationDetailId!.Value,
                EffectiveQuantity = item.NewValue,
                UnitOfMeasurementId = item.UnitOfMeasurementId,
                UnitPrice = item.UnitPrice
            });
    }

    private IQueryable<GetAvailableContractTypeDetailSourcesModel>
        GetAvailableProcurementSources(
            long contractTypeId,
            long projectId,
            long? projectOperationId,
            string? filterData)
    {
        var details = DbSet.AsQueryable().AsNoTracking();

        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var products = DbContext
            .Set<ConsumableVolumeProduct>()
            .AsQueryable()
            .AsNoTracking();

        var groups = DbContext
            .Set<ViewGroup>()
            .AsQueryable()
            .AsNoTracking();

        var query =
            from product in products
            join productGroup in groups
                on product.ProductGroupId equals productGroup.Id
            select new
            {
                product,
                productGroup
            };

        query = query.Where(value =>
            value.product.VolumeProductType ==
            VolumeProductType.ProductGroup);

        query = query.Where(value =>
            value.product.ProjectOperationDetail
                .ProjectOperation.ProjectId == projectId);

        if (projectOperationId.HasValue)
        {
            query = query.Where(value =>
                value.product.ProjectOperationDetail
                    .ProjectOperationId ==
                projectOperationId.Value);
        }

        query = query.Where(value =>
            !details.Any(detail =>
                detail.ContractTypeId == contractTypeId &&
                detail.ConsumableVolumeProductId ==
                value.product.Id));

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            query = query.Where(value =>
                value.productGroup.Name.Contains(filterData) ||
                value.productGroup.Code.Contains(filterData) ||
                value.product.ProjectOperationDetail.Code
                    .Contains(filterData) ||
                value.product.ProjectOperationDetail.ProjectOperation
                    .OperationInfo.OperationInfoName.Contains(filterData) ||
                value.product.ProjectOperationDetail.ProjectOperation
                    .OperationInfo.OperationInfoCode.Contains(filterData));
        }

        return query.Select(value =>
            new GetAvailableContractTypeDetailSourcesModel
            {
                SourceId = value.product.Id,
                ProjectOperationId =
                    value.product.ProjectOperationDetail
                        .ProjectOperationId,
                ProjectOperationName =
                    value.product.ProjectOperationDetail
                        .ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode =
                    value.product.ProjectOperationDetail
                        .ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectOperationDetailId =
                    value.product.ProjectOperationDetail.Id,
                ProjectOperationDetailCode =
                    value.product.ProjectOperationDetail.Code,
                ProjectOperationDetailDescription =
                    value.product.ProjectOperationDetail.Description,
                SourceTitle = value.productGroup.Name,
                SourceCode = value.productGroup.Code,
                SourceDescription = value.productGroup.Description,
                SourceQuantity = value.product.FinalValue,
                AvailableQuantity = value.product.FinalValue -
                    (allocations
                        .Where(detail =>
                            detail.ConsumableVolumeProductId ==
                            value.product.Id)
                        .Sum(detail =>
                            (decimal?)detail.EffectiveQuantity) ?? 0m),
                UnitOfMeasurementId =
                    value.productGroup.MeasureUnitId,
                EstimatedUnitPrice = null,
                IsUnitOfMeasurementReadOnly = true
            });
    }

    private IQueryable<GetAvailableContractTypeDetailSourcesModel>
        GetAvailableConstructionSources(
            long contractTypeId,
            long projectId,
            long contractPartyId,
            long? projectOperationId,
            string? filterData)
    {
        var details = DbSet.AsQueryable().AsNoTracking();

        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var sourceAllocations =
            GetEffectiveSourceBasedConstructionAllocations();

        var query = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        query = query.Where(detail =>
            detail.ProjectOperation.ProjectId == projectId);

        if (projectOperationId.HasValue)
        {
            query = query.Where(detail =>
                detail.ProjectOperationId ==
                projectOperationId.Value);
        }

        query = query.Where(detail =>
            !detail.ProjectOperationDetailContractorServices.Any(service =>
                service.Type == PODContractorServiceType.ServiceBased &&
                service.ContractorId.HasValue &&
                service.ContractorId.Value != contractPartyId));

        query = query.Where(detail =>
            !details.Any(contractDetail =>
                contractDetail.ContractTypeId == contractTypeId &&
                contractDetail.ProjectOperationDetailId == detail.Id));

        var queryWithAmounts =
            query.Select(detail => new
            {
                Detail = detail,
                DeductionQuantity =
                    detail.ProjectOperationDetailDeductions
                        .Sum(deduction =>
                            (decimal?)(
                                deduction.Length *
                                deduction.Width *
                                deduction.Height *
                                deduction.Weight *
                                deduction.Number)) ?? 0m,
                LegacyAssignedQuantity =
                    detail.ProjectOperationDetailContractorServices
                        .Where(service =>
                            service.Type ==
                            PODContractorServiceType.OperationBased)
                        .Sum(service =>
                            (decimal?)service.Volume) ?? 0m,
                ContractAssignedQuantity =
                    allocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            detail.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m,
                ChangeAssignedQuantity =
                    sourceAllocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            detail.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
            });

        queryWithAmounts = queryWithAmounts.Where(value =>
            value.Detail.FinalAmount -
            value.DeductionQuantity -
            value.LegacyAssignedQuantity -
            value.ContractAssignedQuantity -
            value.ChangeAssignedQuantity > 0);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            queryWithAmounts = queryWithAmounts.Where(value =>
                value.Detail.Code.Contains(filterData) ||
                (value.Detail.Description != null &&
                 value.Detail.Description.Contains(filterData)) ||
                value.Detail.ProjectOperation.OperationInfo
                    .OperationInfoName.Contains(filterData) ||
                value.Detail.ProjectOperation.OperationInfo
                    .OperationInfoCode.Contains(filterData));
        }

        return queryWithAmounts.Select(value =>
            new GetAvailableContractTypeDetailSourcesModel
            {
                SourceId = value.Detail.Id,
                ProjectOperationId =
                    value.Detail.ProjectOperationId,
                ProjectOperationName =
                    value.Detail.ProjectOperation.OperationInfo.OperationInfoName,
                ProjectOperationCode =
                    value.Detail.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectOperationDetailId =
                    value.Detail.Id,
                ProjectOperationDetailCode =
                    value.Detail.Code,
                ProjectOperationDetailDescription =
                    value.Detail.Description,
                SourceTitle =
                    value.Detail.Description ??
                    value.Detail.ProjectOperation.OperationInfo.OperationInfoName,
                SourceCode = value.Detail.Code,
                SourceDescription = value.Detail.Description,
                SourceQuantity = value.Detail.FinalAmount,
                AvailableQuantity =
                    value.Detail.FinalAmount -
                    value.DeductionQuantity -
                    value.LegacyAssignedQuantity -
                    value.ContractAssignedQuantity -
                    value.ChangeAssignedQuantity,
                UnitOfMeasurementId =
                    value.Detail.ProjectOperation.UnitOfMeasurementId,
                EstimatedUnitPrice =
                    value.Detail.ProjectOperation.ChangedPrice *
                    value.Detail.ProjectOperation.IncreaseRate,
                IsUnitOfMeasurementReadOnly = true
            });
    }

    private IQueryable<GetAvailableContractTypeDetailSourcesModel>
        GetAvailableServiceSources(
            long contractTypeId,
            long projectId,
            long contractPartyId,
            long? projectOperationId,
            string? filterData)
    {
        var details = DbSet.AsQueryable().AsNoTracking();

        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var query = DbContext
            .Set<ProjectOperationDetailContractorService>()
            .AsQueryable()
            .AsNoTracking();

        query = query.Where(service =>
            service.Type == PODContractorServiceType.ServiceBased &&
            service.OperationInfoServiceId.HasValue &&
            service.ProjectOperationDetail.ProjectOperation.ProjectId ==
            projectId);

        if (projectOperationId.HasValue)
        {
            query = query.Where(service =>
                service.ProjectOperationDetail.ProjectOperationId ==
                projectOperationId.Value);
        }

        query = query.Where(service =>
            !service.ContractorId.HasValue ||
            service.ContractorId.Value == contractPartyId);

        query = query.Where(service =>
            !details.Any(detail =>
                detail.ContractTypeId == contractTypeId &&
                detail.ProjectOperationDetailContractorServiceId ==
                service.Id));

        var queryWithAmounts = query.Select(service => new
        {
            Service = service,
            ContractAssignedQuantity =
                allocations
                    .Where(detail =>
                        detail.ProjectOperationDetailContractorServiceId ==
                        service.Id)
                    .Sum(detail =>
                        (decimal?)detail.EffectiveQuantity) ?? 0m
        });

        queryWithAmounts = queryWithAmounts.Where(value =>
            value.Service.Volume -
            value.ContractAssignedQuantity > 0);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            queryWithAmounts = queryWithAmounts.Where(value =>
                value.Service.OperationInfoService!.ServiceInfo
                    .ServiceInfoName.Contains(filterData) ||
                value.Service.OperationInfoService.ServiceInfo
                    .ServiceInfoCode.Contains(filterData) ||
                value.Service.ProjectOperationDetail.Code.Contains(filterData) ||
                value.Service.ProjectOperationDetail.ProjectOperation
                    .OperationInfo.OperationInfoName.Contains(filterData) ||
                value.Service.ProjectOperationDetail.ProjectOperation
                    .OperationInfo.OperationInfoCode.Contains(filterData));
        }

        return queryWithAmounts.Select(value =>
            new GetAvailableContractTypeDetailSourcesModel
            {
                SourceId = value.Service.Id,
                ProjectOperationId =
                    value.Service.ProjectOperationDetail
                        .ProjectOperationId,
                ProjectOperationName =
                    value.Service.ProjectOperationDetail.ProjectOperation
                        .OperationInfo.OperationInfoName,
                ProjectOperationCode =
                    value.Service.ProjectOperationDetail.ProjectOperation
                        .OperationInfo.OperationInfoCode,
                ProjectOperationDetailId =
                    value.Service.ProjectOperationDetailId,
                ProjectOperationDetailCode =
                    value.Service.ProjectOperationDetail.Code,
                ProjectOperationDetailDescription =
                    value.Service.ProjectOperationDetail.Description,
                SourceTitle =
                    value.Service.OperationInfoService!.ServiceInfo
                        .ServiceInfoName,
                SourceCode =
                    value.Service.OperationInfoService.ServiceInfo
                        .ServiceInfoCode,
                SourceDescription =
                    value.Service.OperationInfoService.ServiceInfo
                        .DescriptionFa,
                SourceQuantity = value.Service.Volume,
                AvailableQuantity =
                    value.Service.Volume -
                    value.ContractAssignedQuantity,
                UnitOfMeasurementId =
                    value.Service.OperationInfoService.ServiceInfo
                        .UnitOfMeasurementId,
                EstimatedUnitPrice = null,
                IsUnitOfMeasurementReadOnly = false
            });
    }
        
    public ContractTypeDetailEntity? GetContractTypeDetailForMutation(
        ContractTypeEntity contractType,
        long detailId) =>
        contractType.ContractTypeDetails
            .FirstOrDefault(detail => detail.Id == detailId);

    public bool HasActiveContractTypeDetailSource(
        ContractTypeEntity contractType,
        long sourceId) =>
        contractType.Kind switch
        {
            ContractTypeKind.Procurement =>
                contractType.ContractTypeDetails.Any(detail =>
                    !detail.IsDeleted &&
                    detail.ConsumableVolumeProductId == sourceId),

            ContractTypeKind.Construction =>
                contractType.ContractTypeDetails.Any(detail =>
                    !detail.IsDeleted &&
                    detail.ProjectOperationDetailId == sourceId),

            ContractTypeKind.Engineering or ContractTypeKind.Services =>
                contractType.ContractTypeDetails.Any(detail =>
                    !detail.IsDeleted &&
                    detail.ProjectOperationDetailContractorServiceId == sourceId),

            _ => false
        };
}
