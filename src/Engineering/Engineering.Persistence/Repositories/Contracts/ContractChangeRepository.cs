using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChanges;
using Engineering.Application.Services.Contracts.Contracts.CreateContractChange;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Application.Services.Contracts.Models.ContractChanges;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;
using Engineering.Application.Services.Contracts.Contracts.ContractChanges;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractChangeRepository
    : BaseRepository<EngineeringDBContext, ContractChange>,
        IContractChangeRepository
{
    public ContractChangeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> HasActiveContractChanges(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(change => change.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(change =>
                change.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<bool> IsContractChangeNumberDuplicate(
        long contractId,
        string number,
        long? excludedContractChangeId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(change => change.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(change =>
                change.Contract.CompanyId == companyId);

        if (!string.IsNullOrWhiteSpace(number))
            query = query.Where(change => change.Number == number);

        if (excludedContractChangeId.HasValue)
            query = query.Where(change =>
                change.Id != excludedContractChangeId.Value);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<List<ContractChangeSourceContextModel>> GetContractChangeSourceContexts(
        long projectId,
        long contractPartyId,
        IReadOnlyCollection<long> consumableVolumeProductIds,
        IReadOnlyCollection<long> projectOperationDetailIds,
        IReadOnlyCollection<long> contractorServiceIds,
        long companyId,
        CT ct)
    {
        var result = new List<ContractChangeSourceContextModel>();
        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        if (consumableVolumeProductIds.Count > 0)
        {
            var procurement = BuildProcurementSourceContextQuery(
                projectId,
                companyId,
                consumableVolumeProductIds,
                allocations);

            result.AddRange(
                await procurement.ToListAsync(ct));
        }

        if (projectOperationDetailIds.Count > 0)
        {
            var sourceAllocations =
                GetEffectiveSourceBasedConstructionAllocations();

            var construction = BuildConstructionSourceContextQuery(
                projectId,
                contractPartyId,
                companyId,
                projectOperationDetailIds,
                allocations,
                sourceAllocations);

            result.AddRange(
                await construction.ToListAsync(ct));
        }

        if (contractorServiceIds.Count > 0)
        {
            var services = BuildServiceSourceContextQuery(
                projectId,
                companyId,
                contractorServiceIds,
                allocations);

            var serviceContexts =
                await services.ToListAsync(ct);

            foreach (var context in serviceContexts)
            {
                result.Add(
                    context with
                    {
                        Kind = ContractTypeKind.Engineering
                    });

                result.Add(
                    context with
                    {
                        Kind = ContractTypeKind.Services
                    });
            }
        }

        return result;
    }

    private IQueryable<ContractChangeSourceContextModel> BuildProcurementSourceContextQuery(
        long projectId,
        long companyId,
        IReadOnlyCollection<long> productIds,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations)
    {
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
            join groupEntity in groups
                on product.ProductGroupId equals groupEntity.Id
            where productIds.Contains(product.Id) &&
                  product.VolumeProductType ==
                  VolumeProductType.ProductGroup &&
                  product.ProjectOperationDetail
                      .ProjectOperation.ProjectId == projectId
            select new
            {
                product,
                groupEntity
            };

        if (companyId > 0)
        {
            query = query.Where(value =>
                value.product.ProjectOperationDetail
                    .ProjectOperation.Project.CompanyId ==
                companyId);
        }

        return query.Select(value =>
            new ContractChangeSourceContextModel(
                ContractTypeKind.Procurement,
                value.product.Id,
                value.product.FinalValue -
                (
                    allocations
                        .Where(item =>
                            item.ConsumableVolumeProductId ==
                            value.product.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
                ),
                value.groupEntity.MeasureUnitId,
                null));
    }

    private IQueryable<ContractChangeSourceContextModel> BuildConstructionSourceContextQuery(
        long projectId,
        long contractPartyId,
        long companyId,
        IReadOnlyCollection<long> sourceIds,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations,
        IQueryable<EffectiveSourceBasedConstructionAllocation> sourceAllocations)
    {
        var query = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        query = query.Where(detail =>
            sourceIds.Contains(detail.Id));

        query = query.Where(detail =>
            detail.ProjectOperation.ProjectId == projectId);

        if (companyId > 0)
        {
            query = query.Where(detail =>
                detail.ProjectOperation.Project.CompanyId == companyId);
        }

        query = query.Where(detail =>
            !detail.ProjectOperationDetailContractorServices.Any(service =>
                service.Type == PODContractorServiceType.ServiceBased &&
                service.ContractorId.HasValue &&
                service.ContractorId.Value != contractPartyId));

        return query.Select(detail =>
            new ContractChangeSourceContextModel(
                ContractTypeKind.Construction,
                detail.Id,
                detail.FinalAmount -
                (
                    detail.ProjectOperationDetailDeductions
                        .Sum(deduction =>
                            (decimal?)(
                                deduction.Length *
                                deduction.Width *
                                deduction.Height *
                                deduction.Weight *
                                deduction.Number)) ?? 0m
                ) -
                (
                    detail.ProjectOperationDetailContractorServices
                        .Where(service =>
                            service.Type ==
                            PODContractorServiceType.OperationBased)
                        .Sum(service =>
                            (decimal?)service.Volume) ?? 0m
                ) -
                (
                    allocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            detail.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
                ) -
                (
                    sourceAllocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            detail.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
                ),
                detail.ProjectOperation.UnitOfMeasurementId,
                detail.ProjectOperation.ChangedPrice *
                detail.ProjectOperation.IncreaseRate));
    }

    private IQueryable<ContractChangeSourceContextModel> BuildServiceSourceContextQuery(
        long projectId,
        long companyId,
        IReadOnlyCollection<long> serviceIds,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations)
    {
        var query = DbContext
            .Set<ProjectOperationDetailContractorService>()
            .AsQueryable()
            .AsNoTracking();

        query = query.Where(service =>
            serviceIds.Contains(service.Id));

        query = query.Where(service =>
            service.Type ==
            PODContractorServiceType.ServiceBased);

        query = query.Where(service =>
            service.OperationInfoServiceId.HasValue);

        query = query.Where(service =>
            service.ProjectOperationDetail
                .ProjectOperation.ProjectId == projectId);

        if (companyId > 0)
        {
            query = query.Where(service =>
                service.ProjectOperationDetail
                    .ProjectOperation.Project.CompanyId ==
                companyId);
        }

        return query.Select(service =>
            new ContractChangeSourceContextModel(
                ContractTypeKind.Services,
                service.Id,
                service.Volume -
                (
                    allocations
                        .Where(item =>
                            item.ProjectOperationDetailContractorServiceId ==
                            service.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
                ),
                service.OperationInfoService!
                    .ServiceInfo.UnitOfMeasurementId,
                null));
    }

    public async Task<GetContractChangeByIdResponse?> GetContractChangeById(
        long contractId,
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(change => change.Contract)
                .ThenInclude(contract => contract.ContractChanges);

        query = query
            .Include(change => change.Documents)
            .Include(change => change.Items)
                .ThenInclude(item => item.ContractTypeDetail)
                    .ThenInclude(detail => detail.ContractType)
            .Include(change => change.Items)
                .ThenInclude(item => item.ContractType);

        if (id > 0)
            query = query.Where(change => change.Id == id);

        if (contractId > 0)
            query = query.Where(change =>
                change.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(change =>
                change.Contract.CompanyId == companyId);

        query = query
            .AsSplitQuery();

        var change = await query.FirstOrDefaultAsync(ct);

        if (change is null)
            return null;

        var sourceData = await GetChangeItemSourceData(
            change.Items,
            ct);

        var response = BuildContractChangeResponse(
            change,
            sourceData);

        return response;
    }

    private async Task<ContractChangeSourceData> GetChangeItemSourceData(
        IReadOnlyCollection<ContractChangeItem> items,
        CT ct)
    {
        var productIds = items
            .Where(item =>
                item.ContractTypeDetail?.ConsumableVolumeProductId
                .HasValue == true)
            .Select(item =>
                item.ContractTypeDetail!.ConsumableVolumeProductId!.Value)
            .Distinct()
            .ToList();

        var serviceIds = items
            .Where(item =>
                item.ContractTypeDetail?
                    .ProjectOperationDetailContractorServiceId
                    .HasValue == true)
            .Select(item =>
                item.ContractTypeDetail!
                    .ProjectOperationDetailContractorServiceId!.Value)
            .Distinct()
            .ToList();

        var operationDetailIds = items
            .Select(item =>
                item.ContractTypeDetail?.ProjectOperationDetailId ??
                item.ProjectOperationDetailId)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

        var products = await GetChangeProducts(
            productIds,
            ct);

        var services = await GetChangeServices(
            serviceIds,
            ct);

        var operationDetails = await GetChangeOperationDetails(
            operationDetailIds,
            ct);

        return new ContractChangeSourceData(
            products,
            services,
            operationDetails);
    }

    private async Task<Dictionary<long, ContractChangeSourceInfo>> GetChangeProducts(
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
            join groupEntity in groups
                on product.ProductGroupId equals groupEntity.Id
            where productIds.Contains(product.Id)
            select new
            {
                product.Id,
                Info = new ContractChangeSourceInfo(
                    groupEntity.Name,
                    groupEntity.Code,
                    groupEntity.Description)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private async Task<Dictionary<long, ContractChangeSourceInfo>> GetChangeServices(
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
                Info = new ContractChangeSourceInfo(
                    service.OperationInfoService!
                        .ServiceInfo.ServiceInfoName,
                    service.OperationInfoService
                        .ServiceInfo.ServiceInfoCode,
                    service.OperationInfoService
                        .ServiceInfo.DescriptionFa)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private async Task<Dictionary<long, ContractChangeSourceInfo>> GetChangeOperationDetails(
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
                Info = new ContractChangeSourceInfo(
                    detail.Description ?? detail.Code,
                    detail.Code,
                    detail.Description)
            })
            .ToListAsync(ct);

        return values.ToDictionary(
            value => value.Id,
            value => value.Info);
    }

    private static GetContractChangeByIdResponse BuildContractChangeResponse(
        ContractChange change,
        ContractChangeSourceData sourceData)
    {
        return new GetContractChangeByIdResponse
        {
            Id = change.Id,
            ContractId = change.ContractId,
            Mode = change.Mode,
            Type = change.Type,
            Number = change.Number,
            Date = change.Date,
            Subject = change.Subject,
            FinancialChangeAmount = change.FinancialChangeAmount,
            DurationChange = change.DurationChange,
            NewContractAmount = change.FinalContractAmount,
            BaselineEndDate = change.Contract.EndDate,
            DurationUnit = change.Contract.DurationUnit,
            CumulativeDurationChange =
                change.Contract.ContractChanges
                    .Where(value =>
                        value.Date < change.Date ||
                        value.Date == change.Date &&
                        value.Id <= change.Id)
                    .Sum(value =>
                        value.DurationChange ?? 0),
            Documents = change.Documents
                .Select(document =>
                    new GetContractChangeDocumentModel
                    {
                        Id = document.Id,
                        Url = document.Url
                    })
                .ToList(),
            Items = change.Items
                .Select(item =>
                    BuildContractChangeItem(
                        item,
                        sourceData))
                .ToList()
        };
    }

    private static GetContractChangeItemModel BuildContractChangeItem(
        ContractChangeItem item,
        ContractChangeSourceData sourceData)
    {
        var detail = item.ContractTypeDetail;

        if (detail is not null && detail.ContractType is null)
            throw new InvalidOperationException(
                $"ContractChangeItem {item.Id}: ContractTypeDetail {detail.Id} has null ContractType.");

        if (detail is null && item.ContractType is null)
            throw new InvalidOperationException(
                $"ContractChangeItem {item.Id}: both ContractTypeDetail and ContractType are null.");

        if (detail is null && !item.ProjectOperationDetailId.HasValue)
            throw new InvalidOperationException(
                $"ContractChangeItem {item.Id}: SourceId cannot be resolved.");

        var contractTypeId = detail?.ContractTypeId ??
            item.ContractTypeId!.Value;

        var sourceId = detail?.ConsumableVolumeProductId ??
            detail?.ProjectOperationDetailId ??
            detail?.ProjectOperationDetailContractorServiceId ??
            item.ProjectOperationDetailId!.Value;

        var kind = detail?.ContractType?.Kind ??
            item.ContractType!.Kind;

        var source = GetChangeItemSource(
            item,
            sourceData);

        return new GetContractChangeItemModel
        {
            Id = item.Id,
            IsOriginalContractItem = detail is not null,
            ContractTypeDetailId = item.ContractTypeDetailId,
            ContractTypeId = contractTypeId,
            SourceId = sourceId,
            Kind = kind,
            PricingMethod = item.PricingMethod,
            SourceTitle = source.Title,
            SourceCode = source.Code,
            SourceDescription = source.Description,
            PreviousValue = item.PreviousValue,
            NewValue = item.NewValue,
            UnitOfMeasurementId = item.UnitOfMeasurementId,
            UnitPrice = item.UnitPrice,
            ChangeAmount = item.ChangeAmount
        };
    }

    private static ContractChangeSourceInfo GetChangeItemSource(
        ContractChangeItem item,
        ContractChangeSourceData sourceData)
    {
        var detail = item.ContractTypeDetail;

        if (detail?.ConsumableVolumeProductId is long productId)
            return sourceData.Products.GetValueOrDefault(

                       productId) ??
                   new ContractChangeSourceInfo(
                       string.Empty,
                       null,
                       null);

        if (detail?.ProjectOperationDetailContractorServiceId
            is long serviceId)
        {
            return sourceData.Services.GetValueOrDefault(
                       serviceId) ??
                   new ContractChangeSourceInfo(
                       string.Empty,
                       null,
                       null);
        }

        var operationDetailId =
            detail?.ProjectOperationDetailId ??
            item.ProjectOperationDetailId;

        if (operationDetailId is long id)
            return sourceData.OperationDetails.GetValueOrDefault(

                       id) ??
                   new ContractChangeSourceInfo(
                       string.Empty,
                       null,
                       null);

        return new ContractChangeSourceInfo(
            string.Empty,
            null,
            null);
    }

    public async Task<(List<GetContractChangesModel> Data, int RowCount)> GetContractChanges(
        long contractId,
        ContractChangeType? type,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(change =>
                change.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(change =>
                change.Contract.CompanyId == companyId);

        if (type.HasValue)
            query = query.Where(change =>
                change.Type == type.Value);

        if (dateFrom.HasValue)
            query = query.Where(change =>
                change.Date >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(change =>
                change.Date <= dateTo.Value);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            query = query.Where(change =>
                change.Number.Contains(filterData) ||
                change.Subject.Contains(filterData));
        }

        query = query.AsNoTracking();

        var resultQuery = query.Select(change =>
            new GetContractChangesModel
            {
                Id = change.Id,
                ContractId = change.ContractId,
                Mode = change.Mode,
                Type = change.Type,
                Number = change.Number,
                Date = change.Date,
                Subject = change.Subject,
                FinancialChangeAmount =
                    change.FinancialChangeAmount,
                DurationChange = change.DurationChange,
                NewContractAmount =
                    change.FinalContractAmount,
                BaselineEndDate =
                    change.Contract.EndDate,
                DurationUnit =
                    change.Contract.DurationUnit,
                CumulativeDurationChange =
                    change.Contract.ContractChanges
                        .Where(value =>
                            value.Date < change.Date ||
                            value.Date == change.Date &&
                            value.Id <= change.Id)
                        .Sum(value =>
                            value.DurationChange ?? 0),
                DocumentCount = change.Documents.Count
            });

        var rowCount = await resultQuery.CountAsync(ct);

        resultQuery = resultQuery
            .OrderByDescending(change => change.Date)
            .ThenByDescending(change => change.Id);

        if (orderBy is { Length: > 0 })
            resultQuery = resultQuery.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            resultQuery = resultQuery.Page(
                pageIndex,
                pageSize);

        var data = await resultQuery.ToListAsync(ct);

        return (data, rowCount);
    }

    public async Task<GetContractChangeAvailableItemsResponse?> GetContractChangeAvailableItems(
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
        var contractType = await GetContractTypeSummary(
            contractId,
            contractTypeId,
            companyId,
            ct);

        if (contractType is null)
            return null;

        var allocations =
            GetEffectiveContractTypeDetailAllocations();

        var sourceAllocations =
            GetEffectiveSourceBasedConstructionAllocations();

        IQueryable<GetContractChangeAvailableItemsModel>? query = null;

        if (contractType.Kind == ContractTypeKind.Procurement)
        {
            query = GetAvailableProcurementChangeItems(
                contractType,
                projectOperationId,
                allocations);
        }
        else if (contractType.Kind == ContractTypeKind.Construction)
        {
            query = GetAvailableConstructionChangeItems(
                contractType,
                projectOperationId,
                allocations,
                sourceAllocations);
        }
        else
        {
            query = GetAvailableServiceChangeItems(
                contractType,
                projectOperationId,
                allocations);
        }

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            query = ApplyChangeAvailableItemSearch(
                query,
                filterData);
        }

        var rowCount = await query.CountAsync(ct);

        query = query
            .OrderByDescending(item =>
                item.IsOriginalContractItem)
            .ThenByDescending(item => item.SourceId);

        if (orderBy is { Length: > 0 })
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return new GetContractChangeAvailableItemsResponse(
            contractId,
            contractTypeId,
            contractType.Kind,
            contractType.PricingMethod,
            data,
            rowCount);
    }

    private async Task<ContractTypeSummary?> GetContractTypeSummary(
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
            query = query.Where(type =>
                type.Id == contractTypeId);

        if (contractId > 0)
            query = query.Where(type =>
                type.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(type =>
                type.Contract.CompanyId == companyId);

        return await query
            .Select(type =>
                new ContractTypeSummary(
                    type.Id,
                    type.ContractId,
                    type.Kind,
                    type.PricingMethod,
                    type.Contract.ProjectId,
                    type.Contract.ContractPartyId))
            .FirstOrDefaultAsync(ct);
    }

    private IQueryable<GetContractChangeAvailableItemsModel> GetAvailableProcurementChangeItems(
        ContractTypeSummary contractType,
        long? projectOperationId,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations)
    {
        var details = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable()
            .AsNoTracking();

        var items = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable()
            .AsNoTracking();

        var products = DbContext
            .Set<ConsumableVolumeProduct>()
            .AsQueryable()
            .AsNoTracking();

        var groups = DbContext
            .Set<ViewGroup>()
            .AsQueryable()
            .AsNoTracking();

        var query =
            from detail in details
            join product in products
                on detail.ConsumableVolumeProductId equals product.Id
            join groupEntity in groups
                on product.ProductGroupId equals groupEntity.Id
            select new
            {
                detail,
                product,
                groupEntity
            };

        query = query.Where(value =>
            value.detail.ContractTypeId == contractType.Id);

        if (projectOperationId.HasValue)
        {
            query = query.Where(value =>
                value.product.ProjectOperationDetail
                    .ProjectOperationId ==
                projectOperationId.Value);
        }

        return query.Select(value => new
        {
            value,
            CurrentValue =
                items
                    .Where(item =>
                        item.ContractTypeDetailId ==
                        value.detail.Id)
                    .OrderByDescending(item =>
                        item.ContractChange.Date)
                    .ThenByDescending(item =>
                        item.ContractChangeId)
                    .Select(item =>
                        (decimal?)item.NewValue)
                    .FirstOrDefault() ??
                (
                    contractType.PricingMethod ==
                    PricingMethod.LumpSum
                        ? value.detail.FixedAmount ?? 0m
                        : value.detail.Quantity
                ),
            CurrentAllocation =
                allocations
                    .Where(item =>
                        item.ContractTypeDetailId ==
                        value.detail.Id)
                    .Select(item =>
                        item.EffectiveQuantity)
                    .FirstOrDefault(),
            TotalAllocation =
                allocations
                    .Where(item =>
                        item.ConsumableVolumeProductId ==
                        value.product.Id)
                    .Sum(item =>
                        (decimal?)item.EffectiveQuantity) ?? 0m
        })
        .Select(value =>
            new GetContractChangeAvailableItemsModel
            {
                IsOriginalContractItem = true,
                ContractTypeDetailId = value.value.detail.Id,
                ContractTypeId = contractType.Id,
                SourceId = value.value.product.Id,
                SourceTitle = value.value.groupEntity.Name,
                SourceCode = value.value.groupEntity.Code,
                SourceDescription = value.value.groupEntity.Description,
                ProjectOperationId =
                    value.value.product.ProjectOperationDetail
                        .ProjectOperationId,
                ProjectOperationDetailId =
                    value.value.product.ProjectOperationDetail.Id,
                CurrentValue = value.CurrentValue,
                UnitOfMeasurementId =
                    value.value.detail.UnitOfMeasurementId,
                UnitPrice = value.value.detail.UnitPrice,
                AvailableQuantity =
                    contractType.PricingMethod == PricingMethod.LumpSum
                        ? null
                        : value.value.product.FinalValue -
                          value.TotalAllocation +
                          value.CurrentAllocation
            });
    }

    private IQueryable<GetContractChangeAvailableItemsModel> GetAvailableConstructionChangeItems(
        ContractTypeSummary contractType,
        long? projectOperationId,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations,
        IQueryable<EffectiveSourceBasedConstructionAllocation> sourceAllocations)
    {
        var details = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable()
            .AsNoTracking();

        var items = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable()
            .AsNoTracking();

        var operationDetails = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        var baselineQuery =
            from detail in details
            join source in operationDetails
                on detail.ProjectOperationDetailId equals source.Id
            select new
            {
                detail,
                source
            };

        baselineQuery = baselineQuery.Where(value =>
            value.detail.ContractTypeId == contractType.Id);

        if (projectOperationId.HasValue)
        {
            baselineQuery = baselineQuery.Where(value =>
                value.source.ProjectOperationId ==
                projectOperationId.Value);
        }

        var originalQuery = baselineQuery
            .Select(value => new
            {
                value,
                CurrentValue =
                    items
                        .Where(item =>
                            item.ContractTypeDetailId ==
                            value.detail.Id)
                        .OrderByDescending(item =>
                            item.ContractChange.Date)
                        .ThenByDescending(item =>
                            item.ContractChangeId)
                        .Select(item =>
                            (decimal?)item.NewValue)
                        .FirstOrDefault() ??
                    (
                        contractType.PricingMethod ==
                        PricingMethod.LumpSum
                            ? value.detail.FixedAmount ?? 0m
                            : value.detail.Quantity
                    ),
                CurrentAllocation =
                    allocations
                        .Where(item =>
                            item.ContractTypeDetailId ==
                            value.detail.Id)
                        .Select(item =>
                            item.EffectiveQuantity)
                        .FirstOrDefault(),
                TotalBaselineAllocation =
                    allocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            value.source.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m,
                TotalChangeAllocation =
                    sourceAllocations
                        .Where(item =>
                            item.ProjectOperationDetailId ==
                            value.source.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m,
                Deductions =
                    value.source.ProjectOperationDetailDeductions
                        .Sum(deduction =>
                            (decimal?)(
                                deduction.Length *
                                deduction.Width *
                                deduction.Height *
                                deduction.Weight *
                                deduction.Number)) ?? 0m,
                Legacy =
                    value.source.ProjectOperationDetailContractorServices
                        .Where(service =>
                            service.Type ==
                            PODContractorServiceType.OperationBased)
                        .Sum(service =>
                            (decimal?)service.Volume) ?? 0m
            })
            .Select(value =>
                new GetContractChangeAvailableItemsModel
                {
                    IsOriginalContractItem = true,
                    ContractTypeDetailId =
                        value.value.detail.Id,
                    ContractTypeId = contractType.Id,
                    SourceId = value.value.source.Id,
                    SourceTitle =
                        value.value.source.Description ??
                        value.value.source.Code,
                    SourceCode = value.value.source.Code,
                    SourceDescription =
                        value.value.source.Description,
                    ProjectOperationId =
                        value.value.source.ProjectOperationId,
                    ProjectOperationDetailId =
                        value.value.source.Id,
                    CurrentValue = value.CurrentValue,
                    UnitOfMeasurementId =
                        value.value.detail.UnitOfMeasurementId,
                    UnitPrice = value.value.detail.UnitPrice,
                    AvailableQuantity =
                        contractType.PricingMethod ==
                        PricingMethod.LumpSum
                            ? null
                            : value.value.source.FinalAmount -
                              value.Deductions -
                              value.Legacy -
                              value.TotalBaselineAllocation -
                              value.TotalChangeAllocation +
                              value.CurrentAllocation
                });

        var query = originalQuery;

        if (contractType.PricingMethod is
            PricingMethod.UnitPrice or
            PricingMethod.TimeAndMaterial)
        {
            var sourceQuery = BuildAdditionalConstructionChangeItems(
                contractType,
                projectOperationId,
                allocations,
                sourceAllocations,
                details);

            query = query.Concat(sourceQuery);
        }

        return query;
    }

    private IQueryable<GetContractChangeAvailableItemsModel> BuildAdditionalConstructionChangeItems(
        ContractTypeSummary contractType,
        long? projectOperationId,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations,
        IQueryable<EffectiveSourceBasedConstructionAllocation> sourceAllocations,
        IQueryable<ContractTypeDetailEntity> details)
    {
        var operationDetails = DbContext
            .Set<ProjectOperationDetail>()
            .AsQueryable()
            .AsNoTracking();

        var query = operationDetails.Select(source => new
        {
            source,
            CurrentValue =
                sourceAllocations
                    .Where(item =>
                        item.ContractId == contractType.ContractId &&
                        item.ContractTypeId == contractType.Id &&
                        item.ProjectOperationDetailId ==
                        source.Id)
                    .Select(item =>
                        (decimal?)item.EffectiveQuantity)
                    .FirstOrDefault() ?? 0m,
            CurrentUnitOfMeasurementId =
                sourceAllocations
                    .Where(item =>
                        item.ContractId == contractType.ContractId &&
                        item.ContractTypeId == contractType.Id &&
                        item.ProjectOperationDetailId ==
                        source.Id)
                    .Select(item => item.UnitOfMeasurementId)
                    .FirstOrDefault(),
            CurrentUnitPrice =
                sourceAllocations
                    .Where(item =>
                        item.ContractId == contractType.ContractId &&
                        item.ContractTypeId == contractType.Id &&
                        item.ProjectOperationDetailId ==
                        source.Id)
                    .Select(item => item.UnitPrice)
                    .FirstOrDefault(),
            TotalBaselineAllocation =
                allocations
                    .Where(item =>
                        item.ProjectOperationDetailId == source.Id)
                    .Sum(item =>
                        (decimal?)item.EffectiveQuantity) ?? 0m,
            TotalChangeAllocation =
                sourceAllocations
                    .Where(item =>
                        item.ProjectOperationDetailId == source.Id)
                    .Sum(item =>
                        (decimal?)item.EffectiveQuantity) ?? 0m,
            Deductions =
                source.ProjectOperationDetailDeductions
                    .Sum(deduction =>
                        (decimal?)(
                            deduction.Length *
                            deduction.Width *
                            deduction.Height *
                            deduction.Weight *
                            deduction.Number)) ?? 0m,
            Legacy =
                source.ProjectOperationDetailContractorServices
                    .Where(service =>
                        service.Type ==
                        PODContractorServiceType.OperationBased)
                    .Sum(service =>
                        (decimal?)service.Volume) ?? 0m
        });

        query = query.Where(value =>
            value.source.ProjectOperation.ProjectId ==
            contractType.ProjectId);

        if (projectOperationId.HasValue)
        {
            query = query.Where(value =>
                value.source.ProjectOperationId ==
                projectOperationId.Value);
        }

        query = query.Where(value =>
            !value.source.ProjectOperationDetailContractorServices
                .Any(service =>
                    service.Type ==
                    PODContractorServiceType.ServiceBased &&
                    service.ContractorId.HasValue &&
                    service.ContractorId.Value !=
                    contractType.ContractPartyId));

        query = query.Where(value =>
            !details.Any(detail =>
                detail.ContractType.ContractId ==
                contractType.ContractId &&
                detail.ProjectOperationDetailId ==
                value.source.Id));

        query = query.Where(value =>
            value.CurrentUnitPrice.HasValue ||
            value.source.ProjectOperation.ChangedPrice *
            value.source.ProjectOperation.IncreaseRate > 0);

        query = query.Where(value =>
            value.CurrentValue > 0 ||
            value.source.FinalAmount -
            value.Deductions -
            value.Legacy -
            value.TotalBaselineAllocation -
            value.TotalChangeAllocation +
            value.CurrentValue > 0);

        return query.Select(value =>
            new GetContractChangeAvailableItemsModel
            {
                IsOriginalContractItem = false,
                ContractTypeDetailId = null,
                ContractTypeId = contractType.Id,
                SourceId = value.source.Id,
                SourceTitle =
                    value.source.Description ??
                    value.source.Code,
                SourceCode = value.source.Code,
                SourceDescription =
                    value.source.Description,
                ProjectOperationId =
                    value.source.ProjectOperationId,
                ProjectOperationDetailId =
                    value.source.Id,
                CurrentValue = value.CurrentValue,
                UnitOfMeasurementId =
                    value.CurrentUnitOfMeasurementId ??
                    value.source.ProjectOperation.UnitOfMeasurementId,
                UnitPrice =
                    value.CurrentUnitPrice ??
                    value.source.ProjectOperation.ChangedPrice *
                    value.source.ProjectOperation.IncreaseRate,
                AvailableQuantity =
                    value.source.FinalAmount -
                    value.Deductions -
                    value.Legacy -
                    value.TotalBaselineAllocation -
                    value.TotalChangeAllocation +
                    value.CurrentValue
            });
    }

    private IQueryable<GetContractChangeAvailableItemsModel> GetAvailableServiceChangeItems(
        ContractTypeSummary contractType,
        long? projectOperationId,
        IQueryable<EffectiveContractTypeDetailAllocation> allocations)
    {
        var details = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable()
            .AsNoTracking();

        var items = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable()
            .AsNoTracking();

        var services = DbContext
            .Set<ProjectOperationDetailContractorService>()
            .AsQueryable()
            .AsNoTracking();

        var query =
            from detail in details
            join service in services
                on detail.ProjectOperationDetailContractorServiceId
                equals service.Id
            select new
            {
                detail,
                service
            };

        query = query.Where(value =>
            value.detail.ContractTypeId == contractType.Id);

        if (projectOperationId.HasValue)
        {
            query = query.Where(value =>
                value.service.ProjectOperationDetail
                    .ProjectOperationId ==
                projectOperationId.Value);
        }

        return query
            .Select(value => new
            {
                value,
                CurrentValue =
                    items
                        .Where(item =>
                            item.ContractTypeDetailId ==
                            value.detail.Id)
                        .OrderByDescending(item =>
                            item.ContractChange.Date)
                        .ThenByDescending(item =>
                            item.ContractChangeId)
                        .Select(item =>
                            (decimal?)item.NewValue)
                        .FirstOrDefault() ??
                    (
                        contractType.PricingMethod ==
                        PricingMethod.LumpSum
                            ? value.detail.FixedAmount ?? 0m
                            : value.detail.Quantity
                    ),
                CurrentAllocation =
                    allocations
                        .Where(item =>
                            item.ContractTypeDetailId ==
                            value.detail.Id)
                        .Select(item =>
                            item.EffectiveQuantity)
                        .FirstOrDefault(),
                TotalAllocation =
                    allocations
                        .Where(item =>
                            item.ProjectOperationDetailContractorServiceId ==
                            value.service.Id)
                        .Sum(item =>
                            (decimal?)item.EffectiveQuantity) ?? 0m
            })
            .Select(value =>
                new GetContractChangeAvailableItemsModel
                {
                    IsOriginalContractItem = true,
                    ContractTypeDetailId =
                        value.value.detail.Id,
                    ContractTypeId = contractType.Id,
                    SourceId = value.value.service.Id,
                    SourceTitle =
                        value.value.service.OperationInfoService!
                            .ServiceInfo.ServiceInfoName,
                    SourceCode =
                        value.value.service.OperationInfoService
                            .ServiceInfo.ServiceInfoCode,
                    SourceDescription =
                        value.value.service.OperationInfoService
                            .ServiceInfo.DescriptionFa,
                    ProjectOperationId =
                        value.value.service.ProjectOperationDetail
                            .ProjectOperationId,
                    ProjectOperationDetailId =
                        value.value.service.ProjectOperationDetailId,
                    CurrentValue = value.CurrentValue,
                    UnitOfMeasurementId =
                        value.value.detail.UnitOfMeasurementId,
                    UnitPrice = value.value.detail.UnitPrice,
                    AvailableQuantity =
                        contractType.PricingMethod ==
                        PricingMethod.LumpSum
                            ? null
                            : value.value.service.Volume -
                              value.TotalAllocation +
                              value.CurrentAllocation
                });
    }

    private static IQueryable<GetContractChangeAvailableItemsModel> ApplyChangeAvailableItemSearch(
        IQueryable<GetContractChangeAvailableItemsModel> query,
        string filterData)
    {
        if (string.IsNullOrWhiteSpace(filterData))
            return query;

        return query.Where(item =>
            item.SourceTitle.Contains(filterData) ||
            (item.SourceCode != null &&
             item.SourceCode.Contains(filterData)) ||
            (item.SourceDescription != null &&
             item.SourceDescription.Contains(filterData)));
    }

    public async Task<ContractChangeMutationContextModel> GetContractChangeMutationContext(
        long contractId,
        long? contractChangeId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(change =>
                change.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(change =>
                change.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        var latest = await GetLatestContractChange(
            query,
            ct);

        if (!contractChangeId.HasValue)
        {
            return new ContractChangeMutationContextModel(
                latest?.Id,
                latest?.Date,
                false,
                null,
                0m,
                null,
                null);
        }

        var target = await GetContractChangeMutationTarget(
            query,
            contractChangeId.Value,
            ct);

        var previousDate = await GetPreviousContractChangeDate(
            query,
            contractChangeId.Value,
            ct);

        return new ContractChangeMutationContextModel(
            latest?.Id,
            latest?.Date,
            target is not null,
            previousDate,
            target?.FinancialChangeAmount ?? 0m,
            target?.DurationChange,
            target?.Mode);
    }

    private static Task<ContractChangeSnapshot?> GetLatestContractChange(
        IQueryable<ContractChange> query,
        CT ct)
    {
        return query
            .OrderByDescending(change => change.Date)
            .ThenByDescending(change => change.Id)
            .Select(change =>
                new ContractChangeSnapshot(
                    change.Id,
                    change.Date))
            .FirstOrDefaultAsync(ct);
    }

    private static Task<ContractChangeMutationTarget?> GetContractChangeMutationTarget(
        IQueryable<ContractChange> query,
        long contractChangeId,
        CT ct)
    {
        return query
            .Where(change => change.Id == contractChangeId)
            .Select(change =>
                new ContractChangeMutationTarget(
                    change.FinancialChangeAmount,
                    change.DurationChange,
                    change.Mode))
            .FirstOrDefaultAsync(ct);
    }

    private static Task<DateTime?> GetPreviousContractChangeDate(
        IQueryable<ContractChange> query,
        long contractChangeId,
        CT ct)
    {
        return query
            .Where(change => change.Id != contractChangeId)
            .OrderByDescending(change => change.Date)
            .ThenByDescending(change => change.Id)
            .Select(change => (DateTime?)change.Date)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<ContractChangeSourceHistoryContextModel>> GetContractChangeSourceHistoryContexts(
        long contractId,
        long? excludedContractChangeId,
        IReadOnlyCollection<long> contractTypeIds,
        IReadOnlyCollection<long> projectOperationDetailIds,
        long companyId,
        CT ct)
    {
        if (contractTypeIds.Count == 0 ||
            projectOperationDetailIds.Count == 0)
        {
            return [];
        }

        var query = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable();

        query = query.Where(item =>
            item.ContractTypeDetailId == null &&
            item.ContractTypeId.HasValue &&
            item.ProjectOperationDetailId.HasValue);

        if (contractId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.Contract.CompanyId ==
                companyId);
        }

        query = query.Where(item =>
            contractTypeIds.Contains(item.ContractTypeId!.Value) &&
            projectOperationDetailIds.Contains(
                item.ProjectOperationDetailId!.Value));

        query = query.AsNoTracking();

        var history = await query
            .Select(item =>
                new ContractChangeSourceHistoryRow(
                    item.ContractTypeId!.Value,
                    item.ProjectOperationDetailId!.Value,
                    item.ContractChangeId,
                    item.ContractChange.Date,
                    item.NewValue,
                    item.UnitOfMeasurementId,
                    item.UnitPrice))
            .ToListAsync(ct);

        return BuildSourceHistoryContexts(
            history,
            excludedContractChangeId);
    }

    private static List<ContractChangeSourceHistoryContextModel> BuildSourceHistoryContexts(
        IReadOnlyCollection<ContractChangeSourceHistoryRow> history,
        long? excludedContractChangeId)
    {
        var result =
            new List<ContractChangeSourceHistoryContextModel>();

        foreach (var group in history.GroupBy(value =>
                     new
                     {
                         value.ContractTypeId,
                         value.ProjectOperationDetailId
                     }))
        {
            var current = group
                .OrderByDescending(value => value.Date)
                .ThenByDescending(value => value.ContractChangeId)
                .First();

            var prior = group
                .Where(value =>
                    !excludedContractChangeId.HasValue ||
                    value.ContractChangeId !=
                    excludedContractChangeId.Value)
                .OrderByDescending(value => value.Date)
                .ThenByDescending(value => value.ContractChangeId)
                .FirstOrDefault();

            result.Add(
                new ContractChangeSourceHistoryContextModel(
                    group.Key.ContractTypeId,
                    group.Key.ProjectOperationDetailId,
                    prior?.NewValue,
                    current.NewValue,
                    prior?.UnitOfMeasurementId,
                    prior?.UnitPrice));
        }

        return result;
    }

    public async Task<List<ContractChangeTypeContextModel>> GetContractChangeTypeContexts(
        long contractId,
        IReadOnlyCollection<long> contractTypeIds,
        long companyId,
        CT ct)
    {
        if (contractTypeIds.Count == 0)
            return [];

        var query = DbContext
            .Set<ContractTypeEntity>()
            .AsQueryable();

        if (contractId > 0)
        {
            query = query.Where(type =>
                type.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(type =>
                type.Contract.CompanyId == companyId);
        }

        query = query.Where(type =>
            contractTypeIds.Contains(type.Id));

        query = query.AsNoTracking();

        return await query
            .Select(type =>
                new ContractChangeTypeContextModel(
                    type.Id,
                    type.Kind,
                    type.PricingMethod))
            .ToListAsync(ct);
    }

    public async Task<List<ContractChangeOmittedItemContextModel>> GetContractChangeOmittedItemContexts(
        long contractId,
        long currentContractChangeId,
        long companyId,
        CT ct)
    {
        var currentItems =
            await GetCurrentOmittedItems(
                contractId,
                currentContractChangeId,
                companyId,
                ct);

        if (currentItems.Count == 0)
            return [];

        var detailIds = currentItems
            .Where(item =>
                item.ContractTypeDetailId.HasValue)
            .Select(item =>
                item.ContractTypeDetailId!.Value)
            .Distinct()
            .ToList();

        var sourceTypeIds = currentItems
            .Where(item =>
                !item.ContractTypeDetailId.HasValue &&
                item.ContractTypeId.HasValue)
            .Select(item => item.ContractTypeId!.Value)
            .Distinct()
            .ToList();

        var sourceIds = currentItems
            .Where(item =>
                !item.ContractTypeDetailId.HasValue)
            .Select(item => item.SourceId)
            .Distinct()
            .ToList();

        var priorItems =
            await GetPriorOmittedItems(
                contractId,
                currentContractChangeId,
                companyId,
                detailIds,
                sourceTypeIds,
                sourceIds,
                ct);

        return BuildOmittedItemContexts(
            currentItems,
            priorItems);
    }

    private async Task<List<CurrentOmittedItem>> GetCurrentOmittedItems(
        long contractId,
        long currentContractChangeId,
        long companyId,
        CT ct)
    {
        var query = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable();

        query = query.Where(item =>
            item.ContractChangeId ==
            currentContractChangeId);

        if (contractId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.Contract.CompanyId ==
                companyId);
        }

        query = query.Where(item =>
            item.PricingMethod != PricingMethod.LumpSum);

        query = query.AsNoTracking();

        return await query
            .Select(item =>
                new CurrentOmittedItem(
                    item.ContractTypeDetailId,
                    item.ContractTypeDetailId.HasValue
                        ? (long?)item.ContractTypeDetail!.ContractTypeId
                        : item.ContractTypeId,
                    item.ContractTypeDetailId.HasValue
                        ? item.ContractTypeDetail!.ContractType.Kind
                        : item.ContractType!.Kind,
                    item.ContractTypeDetailId.HasValue
                        ? item.ContractTypeDetail!.ConsumableVolumeProductId ??
                          item.ContractTypeDetail.ProjectOperationDetailId ??
                          item.ContractTypeDetail
                              .ProjectOperationDetailContractorServiceId ??
                          0
                        : item.ProjectOperationDetailId ?? 0,
                    item.PricingMethod,
                    item.NewValue,
                    item.ContractTypeDetailId.HasValue
                        ? (decimal?)item.ContractTypeDetail!.Quantity
                        : null))
            .ToListAsync(ct);
    }

    private async Task<List<PriorOmittedItem>> GetPriorOmittedItems(
        long contractId,
        long currentContractChangeId,
        long companyId,
        IReadOnlyCollection<long> detailIds,
        IReadOnlyCollection<long> sourceTypeIds,
        IReadOnlyCollection<long> sourceIds,
        CT ct)
    {
        var query = DbContext
            .Set<ContractChangeItem>()
            .AsQueryable();

        if (contractId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(item =>
                item.ContractChange.Contract.CompanyId ==
                companyId);
        }

        query = query.Where(item =>
            item.ContractChangeId !=
            currentContractChangeId);

        query = query.Where(item =>
            (
                item.ContractTypeDetailId.HasValue &&
                detailIds.Contains(
                    item.ContractTypeDetailId.Value)
            ) ||
            (
                item.ContractTypeDetailId == null &&
                item.ContractTypeId.HasValue &&
                sourceTypeIds.Contains(
                    item.ContractTypeId.Value) &&
                item.ProjectOperationDetailId.HasValue &&
                sourceIds.Contains(
                    item.ProjectOperationDetailId.Value)
            ));

        query = query.AsNoTracking();

        return await query
            .Select(item =>
                new PriorOmittedItem(
                    item.ContractTypeDetailId,
                    item.ContractTypeId,
                    item.ProjectOperationDetailId,
                    item.ContractChangeId,
                    item.ContractChange.Date,
                    item.NewValue))
            .ToListAsync(ct);
    }

    private static List<ContractChangeOmittedItemContextModel> BuildOmittedItemContexts(
        IReadOnlyCollection<CurrentOmittedItem> currentItems,
        IReadOnlyCollection<PriorOmittedItem> priorItems)
    {
        var result =
            new List<ContractChangeOmittedItemContextModel>(
                currentItems.Count);

        foreach (var item in currentItems)
        {
            var proposedValue =
                item.ContractTypeDetailId.HasValue
                    ? GetPriorDetailValue(
                        item,
                        priorItems)
                    : GetPriorSourceValue(
                        item,
                        priorItems);

            result.Add(
                new ContractChangeOmittedItemContextModel(
                    item.ContractTypeDetailId,
                    item.ContractTypeId,
                    item.SourceId,
                    item.Kind,
                    item.PricingMethod,
                    item.NewValue,
                    proposedValue));
        }

        return result;
    }

    private static decimal GetPriorDetailValue(
        CurrentOmittedItem item,
        IReadOnlyCollection<PriorOmittedItem> priorItems)
    {
        return priorItems
            .Where(prior =>
                prior.ContractTypeDetailId ==
                item.ContractTypeDetailId)
            .OrderByDescending(prior => prior.Date)
            .ThenByDescending(prior =>
                prior.ContractChangeId)
            .Select(prior =>
                (decimal?)prior.NewValue)
            .FirstOrDefault() ??
            item.BaselineQuantity ??
            0m;
    }

    private static decimal GetPriorSourceValue(
        CurrentOmittedItem item,
        IReadOnlyCollection<PriorOmittedItem> priorItems)
    {
        return priorItems
            .Where(prior =>
                prior.ContractTypeDetailId == null &&
                prior.ContractTypeId == item.ContractTypeId &&
                prior.ProjectOperationDetailId ==
                item.SourceId)
            .OrderByDescending(prior => prior.Date)
            .ThenByDescending(prior =>
                prior.ContractChangeId)
            .Select(prior =>
                (decimal?)prior.NewValue)
            .FirstOrDefault() ??
            0m;
    }

    private IQueryable<EffectiveContractTypeDetailAllocation> GetEffectiveContractTypeDetailAllocations()
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

        var details = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable()
            .AsNoTracking();

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

    private IQueryable<EffectiveSourceBasedConstructionAllocation> GetEffectiveSourceBasedConstructionAllocations()
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
                later.ContractTypeId ==
                item.ContractTypeId &&
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
                ContractId =
                    item.ContractChange.ContractId,
                ContractTypeId =
                    item.ContractTypeId!.Value,
                ProjectOperationDetailId =
                    item.ProjectOperationDetailId!.Value,
                EffectiveQuantity =
                    item.NewValue,
                UnitOfMeasurementId =
                    item.UnitOfMeasurementId,
                UnitPrice = item.UnitPrice
            });
    }
        
    public async Task<List<ContractChangeSourceContextModel>> GetContractChangeSourceContexts(
        long projectId,
        long contractPartyId,
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId)> requestedSources,
        long companyId,
        CT ct)
    {
        return await GetContractChangeSourceContexts(
            projectId,
            contractPartyId,
            requestedSources
                .Where(value => value.Kind == ContractTypeKind.Procurement)
                .Select(value => value.SourceId)
                .Distinct()
                .ToList(),
            requestedSources
                .Where(value => value.Kind == ContractTypeKind.Construction)
                .Select(value => value.SourceId)
                .Distinct()
                .ToList(),
            requestedSources
                .Where(value =>
                    value.Kind is
                        ContractTypeKind.Engineering or
                        ContractTypeKind.Services)
                .Select(value => value.SourceId)
                .Distinct()
                .ToList(),
            companyId,
            ct);
    }

    public ContractChangeRequestProjection GetContractChangeRequestProjection(
        IReadOnlyCollection<ContractChangeItemRequest> requests)
    {
        var detailIds = requests
            .Where(item => item.ContractTypeDetailId.HasValue)
            .Select(item => item.ContractTypeDetailId!.Value)
            .Distinct()
            .ToList();

        var sourceItems = requests
            .Where(item => item.SourceItem is not null)
            .Select(item => item.SourceItem!);

        var sourceContractTypeIds = sourceItems
            .Select(item => item.ContractTypeId)
            .Distinct()
            .ToList();

        var sourceIds = sourceItems
            .Select(item => item.SourceId)
            .Distinct()
            .ToList();

        var replacementSourceKeys = sourceItems
            .Select(item => (item.ContractTypeId, item.SourceId))
            .ToHashSet();

        var duplicateDetailIds = requests
            .Where(item => item.ContractTypeDetailId.HasValue)
            .GroupBy(item => item.ContractTypeDetailId!.Value)
            .Any(group => group.Count() > 1);

        var duplicateSources = sourceItems
            .GroupBy(item => new
            {
                item.ContractTypeId,
                item.SourceId
            })
            .Any(group => group.Count() > 1);

        return new ContractChangeRequestProjection(
            duplicateDetailIds || duplicateSources,
            detailIds,
            sourceContractTypeIds,
            sourceIds,
            replacementSourceKeys);
    }

    public decimal CalculateContractChangeAmount(
        IReadOnlyCollection<ContractChangeItemTerms> itemTerms) =>
        itemTerms.Sum(item =>
            ContractChangeItem.CalculateChangeAmount(
                item.PricingMethod,
                item.PreviousValue,
                item.NewValue,
                item.UnitPrice));

    public ContractChangeCapacityValidationStatus ValidateContractChangeCapacityTransitions(
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> transitions,
        IReadOnlyCollection<ContractChangeSourceContextModel> sourceContexts)
    {
        foreach (var capacityGroup in transitions.GroupBy(value => new
        {
            Kind = NormalizeCapacityKind(value.Kind),
            value.SourceId
        }))
        {
            var source = sourceContexts.FirstOrDefault(context =>
                NormalizeCapacityKind(context.Kind) == capacityGroup.Key.Kind &&
                context.SourceId == capacityGroup.Key.SourceId);

            if (source is null)
                return ContractChangeCapacityValidationStatus.SourceMissing;

            var totalDelta = capacityGroup.Sum(value => value.Delta);

            if (totalDelta > 0 && totalDelta > source.AvailableQuantity)
                return ContractChangeCapacityValidationStatus.ExceedsAvailableQuantity;
        }

        return ContractChangeCapacityValidationStatus.Valid;
    }

    public ContractChangeDetailResolutionContextModel? FindContractChangeDetailContext(
        IReadOnlyCollection<ContractChangeDetailResolutionContextModel> contexts,
        long detailId) =>
        contexts.FirstOrDefault(context =>
            context.ContractTypeDetailId == detailId);

    public ContractChangeTypeContextModel? FindContractChangeTypeContext(
        IReadOnlyCollection<ContractChangeTypeContextModel> contexts,
        long contractTypeId) =>
        contexts.FirstOrDefault(context =>
            context.ContractTypeId == contractTypeId);

    public ContractChangeSourceHistoryContextModel? FindContractChangeSourceHistoryContext(
        IReadOnlyCollection<ContractChangeSourceHistoryContextModel> contexts,
        long contractTypeId,
        long sourceId) =>
        contexts.FirstOrDefault(context =>
            context.ContractTypeId == contractTypeId &&
            context.ProjectOperationDetailId == sourceId);

    public ContractChangeSourceContextModel? FindContractChangeSourceContext(
        IReadOnlyCollection<ContractChangeSourceContextModel> contexts,
        ContractTypeKind kind,
        long sourceId) =>
        contexts.FirstOrDefault(context =>
            NormalizeCapacityKind(context.Kind) == NormalizeCapacityKind(kind) &&
            context.SourceId == sourceId);

    public IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> BuildOmittedCapacityTransitions(
        IReadOnlyCollection<ContractChangeOmittedItemContextModel> omittedContexts,
        IReadOnlyCollection<long> replacementContractTypeDetailIds,
        IReadOnlyCollection<(long ContractTypeId, long SourceId)> replacementSourceKeys) =>
        omittedContexts
            .Where(context =>
                context.ContractTypeDetailId.HasValue
                    ? !replacementContractTypeDetailIds.Contains(
                        context.ContractTypeDetailId.Value)
                    : context.ContractTypeId.HasValue &&
                      !replacementSourceKeys.Contains(
                        (context.ContractTypeId.Value, context.SourceId)))
            .Select(context => (
                context.Kind,
                context.SourceId,
                Delta: context.ProposedValue - context.CurrentValue))
            .ToList();

    private static ContractTypeKind NormalizeCapacityKind(ContractTypeKind kind) =>
        kind == ContractTypeKind.Services
            ? ContractTypeKind.Engineering
            : kind;

    public IReadOnlyCollection<(ContractTypeKind Kind, long SourceId)> GetCapacityTransitionSourceKeys(
        IReadOnlyCollection<(ContractTypeKind Kind, long SourceId, decimal Delta)> transitions) =>
        transitions
            .Select(value => (value.Kind, value.SourceId))
            .Distinct()
            .ToList();
}
