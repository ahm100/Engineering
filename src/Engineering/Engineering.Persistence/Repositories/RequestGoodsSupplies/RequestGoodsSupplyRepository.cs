using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class RequestGoodsSupplyRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupply>, IRequestGoodsSupplyRepository
{
    public RequestGoodsSupplyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplies(List<long>? ids, List<long>? detailIds, List<long>? managementIds,
        List<long>? costCenterIds, List<long>? projectIds, long? cityId, long? projectManagerId, List<long>? projectOperationIds, List<long>? projectOperationDetailIds,
        string? filterData, List<long>? creatorIds, List<long>? productIds, List<GoodsSupplyStatus>? statuses, List<GoodsSupplyStatus>? removeStatuses, List<GoodsSupplyType>? types,
        DateTime? fromDate, DateTime? toDate, string? filterDescription, string? filterPublicName, string? filterOperationInfoName, long? companyId, string[]? orderBy,
        int pageIndex, int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ProjectOperation.OperationInfo)
            .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

            .Where(c =>
                (companyId == null || c.CompanyId == companyId) &&
                (productIds == null || productIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => productIds.Contains(x.ProductId))) &&
                (creatorIds == null || creatorIds.Count == 0 || creatorIds.Contains(c.CreatorId)) &&
                (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                (detailIds == null || detailIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => detailIds.Contains(x.Id))) &&
                (managementIds == null || managementIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => x.RequestGoodsSupplyManagements.Any(m => managementIds.Contains(m.Id)))) &&
                (costCenterIds == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(c.ProjectOperation.Project.Id)) &&
                (cityId == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (projectManagerId == null || c.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (projectOperationIds == null || projectOperationIds.Contains(c.ProjectOperation.Id)) &&
                (types == null || types.Contains(c.Type)) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)) &&
                (fromDate == null || c.Created >= fromDate) &&
                (toDate == null || c.Created <= toDate) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.SerialNumber.ToString() + "-" + c.Id.ToString(), filterData.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(c.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterDescription) || c.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern()))) &&
                (string.IsNullOrWhiteSpace(filterPublicName) || c.RequestGoodsSupplyDetails.Any(x => EF.Functions.Like(x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern()))) &&
                (projectOperationDetailIds == null || projectOperationDetailIds.Contains(c.ProjectOperationDetail.Id) ||
                c.RequestGoodsSupplyDetails.Any(x => projectOperationDetailIds.Contains(x.ConsumableVolumeProduct.ProjectOperationDetail.Id)))
                );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetsRequestGoodsSupply(List<long>? costCenterIds, List<long>? projectIds, long? cityId, long? projectManagerId, long? projectOperationId, long? projectOperationDetailId,
        string? filterData, long? creatorId, List<GoodsSupplyStatus>? statuses, List<GoodsSupplyStatus>? removeStatuses, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ProjectOperation.OperationInfo)
            .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)

            .Where(c =>
                (companyId == null || c.CompanyId == companyId) &&
                (creatorId == null || c.CreatorId.Equals(creatorId)) &&
                (cityId == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (costCenterIds == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(c.ProjectOperation.Project.Id)) &&
                (projectManagerId == null || c.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (projectOperationId == null || c.ProjectOperation.Id.Equals(projectOperationId)) &&
                (projectOperationDetailId == null || c.ProjectOperationDetail.Id.Equals(projectOperationDetailId)) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.SerialNumber.ToString() + "-" + c.Id.ToString(), filterData.MakeLikePattern()))
                );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplyManagement(List<long>? ids, List<long>? costCenterIds, List<long>? projectIds,
        long? projectManagerId, List<long>? creatorIds, List<long>? productIds, DateTime? fromDate, DateTime? toDate, List<GoodsSupplyStatus>? statuses, List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types, List<GoodsSupplyType>? removeTypes, string? filterData, long? companyId, string? customerInvoiceNumber, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
                         .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetail!.OperationLocation)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

            .Where(c =>
                (companyId == null || c.CompanyId == companyId) &&
                (costCenterIds == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(c.ProjectOperation.Project.Id)) &&
                (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                (projectManagerId == null || c.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (creatorIds == null || creatorIds.Count == 0 || creatorIds.Contains(c.CreatorId)) &&
                (productIds == null || productIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => productIds.Contains(x.ProductId))) &&
                (fromDate == null || c.Created >= fromDate) &&
                (toDate == null || c.Created <= toDate) &&
                (types == null || types.Contains(c.Type)) &&
                (removeTypes == null || !removeTypes.Contains(c.Type)) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)) &&
                (customerInvoiceNumber == null || c.RequestGoodsSupplyDetails.Any(x => x.CustomerInvoiceNumber == customerInvoiceNumber)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.SerialNumber + "-" + c.Id.ToString(), filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSupplies(List<long>? ids, List<long>? costCenterIds, List<long>? projectIds,
    long projectManagerId, List<long>? creatorIds, List<long>? productIds, DateTime? fromDate, DateTime? toDate, List<GoodsSupplyStatus>? statuses, List<GoodsSupplyStatus>? removeStatuses,
    List<GoodsSupplyType>? types, string? filterData, long? companyId, string? customerInvoiceNumber, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
                         .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetail!.OperationLocation)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

            .Where(c =>
                (companyId == null || c.CompanyId == companyId) &&
                (costCenterIds == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (projectIds == null || projectIds.Contains(c.ProjectOperation.Project.Id)) &&
                (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                (c.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                (creatorIds == null || creatorIds.Count == 0 || creatorIds.Contains(c.CreatorId)) &&
                (productIds == null || productIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => productIds.Contains(x.ProductId))) &&
                (fromDate == null || c.Created >= fromDate) &&
                (toDate == null || c.Created <= toDate) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (types == null || types.Contains(c.Type)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)) &&
                (customerInvoiceNumber == null || c.RequestGoodsSupplyDetails.Any(x => x.CustomerInvoiceNumber == customerInvoiceNumber)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.SerialNumber + "-" + c.Id.ToString(), filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }


    public async Task<List<RequestGoodsSupply>?> GetRequestGoodsSupplyByDate(DateTime startDate, DateTime endDate, GoodsSupplyType? type, List<long> projectOperationIds, CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.ConsumableVolumeProduct)
                            .ThenInclude(oo => oo.ProjectOperationDetail.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperation)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                         .Where(oo => oo.Created.Date >= startDate.Date &&
                                     oo.Created.Date <= endDate.Date &&
                                     (type == null || oo.Type.Equals(type)) &&
                                     projectOperationIds.Contains(oo.ProjectOperation!.Id)
                                     )
            .OrderByDescending(oo => oo.Created);
        return await query.ToListAsync(ct);
    }

    public async Task<List<long>> GetsRequestGoodSupplyRequester(CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.ConsumableVolumeProduct)
                         .Include(oo => oo.ProjectOperation)

            .OrderByDescending(oo => oo.Created)
            .Select(l => l.CreatorId).Distinct();
        return await query.ToListAsync(ct);
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplyById(long id, CT ct)
    {
        var query = DbSet
                         .Include(oo => oo.RequestGoodsSupplyProducts)
                            .ThenInclude(oo => oo.RequestGoodsSupplyDetails)
                         .Include(oo => oo.ProjectOperation)
                            .ThenInclude(oo => oo.OperationInfo)
                            .ThenInclude(oo => oo.OperationInfoSeasons)
                            .ThenInclude(oo => oo.Season.Branch.Category)
                         .Include(oo => oo.ProjectOperation)
                            .ThenInclude(oo => oo.Project)
                                .ThenInclude(oo => oo.ProjectCostCenters)
                                    .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo!.OperationLocation)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.ConsumableVolumeProduct)
                                .ThenInclude(oo => oo.ProjectOperationDetail)
                                    .ThenInclude(oo => oo.OperationLocation)
                         .Include(oo => oo.RequestGoodsSupplyProducts)
                            .ThenInclude(oo => oo.RequestGoodsSupplyManagements)
                         .Include(oo => oo.RequestGoodsSupplyDetails)
                            .ThenInclude(oo => oo.ProjectProduct)
                        .Include(oo => oo.Project)
                            .ThenInclude(oo => oo.ProjectCostCenters)
                                .ThenInclude(oo => oo.CostCenter)
                        .Include(oo => oo.Project)
                            .ThenInclude(oo => oo.ProjectCategories)
                                .ThenInclude(oo => oo.Category)
                         .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetById(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Include(oo => oo.RequestGoodsSupplyDetails)
            .Include(oo => oo.RequestGoodsSupplyTypes)
                .ThenInclude(oo => oo.RequestGoodsSupplyTypeDetails)
             .Include(oo => oo.RequestGoodsSupplyTypes)
                .ThenInclude(oo => oo.RequestGoodsSupplyTypeDocuments)
            .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplyByIdForSeason(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(oo => oo.OperationInfo)
                .ThenInclude(oo => oo.OperationInfoSeasons)
                .ThenInclude(oo => oo.Season)

            .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplySummary(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyManagements)
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(oo => oo!.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ConsumableVolumeProduct)
                    .ThenInclude(oo => oo.ProjectOperationDetail)

            .Where(c =>
                   c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplyByInvoiceId(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

                .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplyForChangeStatus(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

                .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<RequestGoodsSupply?> GetRequestGoodsSupplyByIdForChangeStatus(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetails)

            .Where(c => c.Id.Equals(id));

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredCreators(List<long>? costCenterIds, List<long>? projectIds,
        List<long>? projectOperationIds, List<long>? projectOperationDetailIds, List<long>? requestGoodsSupplyIds, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
        .Where(oo => !oo.IsDeleted &&
        oo.CreatorId != 0 &&
        (requestGoodsSupplyIds == null || requestGoodsSupplyIds.Count == 0 || requestGoodsSupplyIds.Contains(oo.Id)) &&
        (projectOperationDetailIds == null || projectOperationDetailIds.Count == 0 || projectOperationDetailIds.Contains(oo.ProjectOperationDetail.Id)) &&
        (costCenterIds == null || costCenterIds.Count == 0 || oo.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
        (projectIds == null || projectIds.Count == 0 || projectIds.Contains(oo.ProjectOperation.Project.Id)) &&
        (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(oo.ProjectOperation.Id)))
        .Select(x => x.CreatorId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetsFilteredProductGroup(long? costCenterId, long? projectId, long? projectOperationId, long? projectOperationDetailId, VolumeProductType? productType, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ConsumableVolumeProduct)
        .Where(oo => !oo.IsDeleted &&
                     oo.CreatorId != 0 &&
                     (projectOperationDetailId == null || oo.ProjectOperationDetail.Id == projectOperationDetailId) &&
                     (costCenterId == null || oo.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                     (projectId == null || oo.ProjectOperation.Project.Id == projectId) &&
                     (projectOperationId == null || oo.ProjectOperation.Id == projectOperationId) &&
                     (productType == null || oo.RequestGoodsSupplyDetails.Any(x => x.ConsumableVolumeProduct.VolumeProductType == productType)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredProducts(List<long>? costCenterIds, List<long>? projectIds, List<long>? projectOperationIds, List<long>? projectOperationDetailIds, List<long>? requestGoodsSupplyIds, List<long>? productGroupIds, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
        .Where(oo => !oo.IsDeleted &&
        oo.CreatorId != 0 &&
        (oo.RequestGoodsSupplyDetails != null && oo.RequestGoodsSupplyDetails.Count > 0) &&
        (requestGoodsSupplyIds == null || requestGoodsSupplyIds.Count == 0 || requestGoodsSupplyIds.Contains(oo.Id)) &&
        (projectOperationDetailIds == null || projectOperationDetailIds.Count == 0 || projectOperationDetailIds.Contains(oo.ProjectOperationDetail.Id)) &&
        (costCenterIds == null || costCenterIds.Count == 0 || oo.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
        (projectIds == null || projectIds.Count == 0 || projectIds.Contains(oo.ProjectOperation.Project.Id)) &&
        (projectOperationIds == null || projectOperationIds.Count == 0 || projectOperationIds.Contains(oo.ProjectOperation.Id)) &&
        (productGroupIds == null || productGroupIds.Count == 0 || oo.RequestGoodsSupplyDetails.Any(x => productGroupIds.Contains(x.ConsumableVolumeProduct.ProductGroupId))))
        .SelectMany(x => x.RequestGoodsSupplyDetails).Where(x => x.ProductId > 0).Select(x => x.ProductId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupply> Data, int RowCount)> GetFilteredRequestGoodsSuppliesReports(
        List<long>? ids,
        List<long>? detailIds,
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        long? projectOperationDetailId,
        List<GoodsSupplyStatus>? statuses,
        List<long>? productIds,
        GoodsSupplyManagementType? type,
        VolumeProductType? productType,
        long? productGroupId,
        DateTime? fromDate,
        DateTime? toDate,
        long? creatorId,
        long? companyId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ProjectOperation.OperationInfo)
            .Include(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyDetails)
                .ThenInclude(oo => oo.RequestGoodsSupplyManagements)

            .Where(c =>
                (productIds == null || c.RequestGoodsSupplyDetails.Any(x => productIds.Contains(x.ProductId))) &&
                (creatorId == null || c.CreatorId == creatorId) &&
                (companyId == null || c.CompanyId == companyId) &&
                (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                (detailIds == null || detailIds.Count == 0 || c.RequestGoodsSupplyDetails.Any(x => detailIds.Contains(x.Id))) &&
                (costCenterId == null || c.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || c.ProjectOperation.Project.Id == projectId) &&
                (projectOperationId == null || c.ProjectOperation.Id.Equals(projectOperationId)) &&
                (projectOperationDetailId == null || c.ProjectOperationDetail.Id.Equals(projectOperationDetailId)) && (statuses == null || statuses.Contains(c.Status)) &&
                (fromDate == null || c.Created >= fromDate) &&
                (toDate == null || c.Created <= toDate) &&
                (type == null || c.RequestGoodsSupplyDetails.Any(x => x.RequestGoodsSupplyManagements.Any(m => m.Type == type))) &&
                (productType == null || c.RequestGoodsSupplyDetails.Any(x => x.ConsumableVolumeProduct.VolumeProductType == productType)) &&
                (productGroupId == null || c.RequestGoodsSupplyDetails.Any(x => x.ConsumableVolumeProduct.ProductGroupId == productGroupId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.SerialNumber.ToString() + "-" + c.Id.ToString(), filterData.MakeLikePattern())));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);

    }

    public async Task<string?> GetProjectNameRequestGoodsSupply(long id, CT ct)
    {
        var query = await DbSet.Where(x => x.RequestGoodsSupplyProducts.Any(x => x.Id == id)).Select(x => x.ProjectOperation.Project.ProjectName).FirstOrDefaultAsync(ct);
        return query;
    }

    public async Task<bool> DoesProjectProductHaveRequestGoodsSupply(List<long> projectProductIds, CT ct)
    {
        var query = await DbSet.Where(x => x.RequestGoodsSupplyProducts.Any(x => x.RequestGoodsSupplyDetails.Where(x => x.ProjectProductId.HasValue).Any(x => projectProductIds.Contains(x.ProjectProductId!.Value)))).FirstOrDefaultAsync(ct);
        return query is null ? false : true;
    }

    public async Task<List<GetFltrRGSupplyWithProductsModel>> GetFltrRGSWithProducts(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? productGroupIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? managementIds,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        long? projectManagerId,
        long? thirdPartyId,
        bool checkThirdParty,
        long? cityId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        long? companyId, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet.Where(c =>
            (productIds == null || productIds.Count == 0 ||
                c.RequestGoodsSupplyProducts.Any(x => productIds.Contains(x.ProductId))) &&

            (types == null || types.Count == 0 || types.Contains(c.Type)) &&

            (companyId == null || c.CompanyId == companyId) &&

            (creatorIds == null || creatorIds.Count == 0 || creatorIds.Contains(c.CreatorId)) &&

            (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&

            (managementIds == null || managementIds.Count == 0 ||
                c.RequestGoodsSupplyDetails.Any(x =>
                    x.RequestGoodsSupplyManagements.Any(m => managementIds.Contains(m.Id)))) &&

            (costCenterIds == null || costCenterIds.Count == 0 ||
                (c.Project != null
                    ? costCenterIds.Contains(c.Project.ProjectCostCenters.FirstOrDefault().CostCenterId)
                    : costCenterIds.Contains(c.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenterId))) &&

            (projectIds == null || projectIds.Count == 0 ||
                (c.Project != null
                    ? projectIds.Contains(c.Project.Id)
                    : projectIds.Contains(c.ProjectOperation.ProjectId))) &&

            (cityId == null ||
                (c.Project != null
                    ? c.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CityId == cityId
                    : c.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CityId == cityId)) &&

            (projectManagerId == null ||
                (c.Project != null
                    ? c.Project.ProjectManager == projectManagerId
                    : c.ProjectOperation.Project.ProjectManager == projectManagerId)) &&

            (statuses == null || statuses.Count == 0 ||
                c.RequestGoodsSupplyProducts.All(d => statuses.Contains(d.Status))) &&

            (removeStatuses == null || removeStatuses.Count == 0 ||
                c.RequestGoodsSupplyProducts.All(d => !removeStatuses.Contains(d.Status))) &&

            (startDate == null || c.RequestedDate >= startDate) &&
            (endDate == null || c.RequestedDate <= endDate) &&

            (string.IsNullOrWhiteSpace(filterData) ||
               EF.Functions.Like(
                    c.SerialNumber.ToString() + "-" + c.Id.ToString(),
                    filterData.MakeLikePattern())) &&

            (checkThirdParty == false ||
            (c.IsProjectSupply && (!c.Project.ProjectThirdParties.Any() || c.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId))) ||
            (!c.IsProjectSupply && (!c.ProjectOperation.Project.ProjectThirdParties.Any() || c.ProjectOperation.Project.ProjectThirdParties.Any(x => x.AuthorizedThirdPartyId == thirdPartyId))))
            )

            .Select(x => new GetFltrRGSupplyWithProductsModel
            {
                Id = x.Id,
                RequestNumber = x.RequestSerialNumber,
                CostCenterId = x.IsProjectSupply ? x.Project.ProjectCostCenters.FirstOrDefault().CostCenterId : x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenterId,
                CostCenterName = x.IsProjectSupply ? x.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName : x.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                ProjectId = x.IsProjectSupply ? x.ProjectId : x.ProjectOperation.ProjectId,
                ProjectName = x.IsProjectSupply ? x.Project.ProjectName : x.ProjectOperation.Project.ProjectName,
                OperationInfoName = x.IsProjectSupply ? null : x.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = x.IsProjectSupply ? null : x.ProjectOperation.OperationInfo.OperationInfoCode,
                PrivateName = x.ProjectOperationDetail == null ? null : x.ProjectOperationDetail.OperationLocation.PrivateName,
                PrivateCode = x.ProjectOperationDetail == null ? null : x.ProjectOperationDetail.OperationLocation.PrivateCode,
                PublicName = x.ProjectOperationDetail == null ? null : x.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = x.ProjectOperationDetail == null ? null : x.ProjectOperationDetail.OperationLocation.PublicCode,
                Workload = x.IsProjectSupply ? null : x.ProjectOperation.Workload,
                Type = x.Type,
                Status = x.Status,
                MeasureId = x.ProjectOperation.OperationInfo.UnitOfMeasurementId,
                IsPettyCash = x.IsPettyCash,
                DeliveryDeadline = x.DeliveryDeadline,
                RegistrationNumber = x.RegistrationNumber,
                RequestingOrganizationId = x.RequestingOrganizationId,
                DescriptionEn = x.DescriptionEn,
                ConsumptionRateAndInventoryUrl = x.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = x.ConsumptionAddress,
                PurchaseLocation = x.PurchaseLocation,
                PurchaseReason = x.PurchaseReason,
                IsProjectSupply = x.IsProjectSupply,
                CreatedOn = x.Created,
                RequestedDate = x.RequestedDate,
                CreatorId = x.CreatorId
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(e => e.CreatedOn);
        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items);
    }

    public async Task<RequestGoodsSupply?> GetRequestProducts(
    long requestGoodsSupplyId,
    CT ct)
    {
        return await DbSet
            .Include(c => c.RequestGoodsSupplyProducts)
            .FirstOrDefaultAsync(c => c.Id == requestGoodsSupplyId, ct);
    }

    public async Task<long?> GetProjectManagerId(
    long Id, CT ct)
    {
        return await DbSet
            .Where(c => c.Id == Id)
            .Select(x => x.IsProjectSupply ? x.Project.ProjectManager : x.ProjectOperation.Project.ProjectManager)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<GetRGSByIdResponse?> GetRGSById(
    long id, CT ct)
    {
        return await DbSet.Where(x => x.Id == id)
            .Select(x => new GetRGSByIdResponse
            {
                Id = x.Id,
                RequestSerialNumber = x.RequestSerialNumber,
                SerialNumber = x.SerialNumber,
                Status = x.Status,
                Type = x.Type,
                PurchaseLocation = x.PurchaseLocation,
                PurchaseReason = x.PurchaseReason,
                ServiceReasonType = x.ServiceReasonType,
                SupplyerId = x.SupplyerId,
                BuyerId = x.BuyerId,
                CurrencyId = x.CurrencyId,
                TransferPrice = x.TransferPrice,
                OtherPrice = x.OtherPrice,
                DiscountOnInvoicePercentage = x.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = x.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = x.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = x.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = x.TaxOnInvoiceNumber,
                FinalInvoiceAmount = x.FinalInvoiceAmount,
                RequestedDate = x.RequestedDate,
                DeliveryDeadline = x.DeliveryDeadline,
                RegistrationNumber = x.RegistrationNumber,
                RequestingOrganizationId = x.RequestingOrganizationId,
                DescriptionEn = x.DescriptionEn,
                Description = x.Description,
                DeviceName = x.DeviceName,
                DeviceEnName = x.DeviceEnName,
                DeviceCode = x.DeviceCode,

                RequestProjectCode = x.Type == GoodsSupplyType.ProjectItems &&
                    x.RequestGoodsSupplyTypes.Any(x => x.Type == SupplyType.Project) ?
                    x.RequestGoodsSupplyTypes.FirstOrDefault(x => x.Type == SupplyType.Project)!.ProjectCode : null,

                RequestProjectName = x.Type == GoodsSupplyType.ProjectItems &&
                    x.RequestGoodsSupplyTypes.Any(x => x.Type == SupplyType.Project) ?
                    x.RequestGoodsSupplyTypes.FirstOrDefault(x => x.Type == SupplyType.Project)!.ProjectName : null,

                RequestProjectEnName = x.Type == GoodsSupplyType.ProjectItems &&
                    x.RequestGoodsSupplyTypes.Any(x => x.Type == SupplyType.Project) ?
                    x.RequestGoodsSupplyTypes.FirstOrDefault(x => x.Type == SupplyType.Project)!.ProjectEnName : null,

                UnitCode = x.UnitCode,
                DeviceNumber = x.DeviceNumber,
                IsPettyCash = x.IsPettyCash,
                ConsumptionRateAndInventoryUrl = x.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = x.ConsumptionAddress,
                IsProjectSupply = x.IsProjectSupply,
                ProjectId = x.ProjectId,
                TypeCount = x.RequestGoodsSupplyTypes.Count,
                TypeDetailCount = x.RequestGoodsSupplyTypeDetails.Count,
                Project = x.ProjectId != null ? x.Project!.ProjectName : null,
                CreatorId = x.CreatorId,
                DetailModels = x.RequestGoodsSupplyTypes.Select(x => new GetRGSByIdTypeModel
                {
                    Id = x.Id,
                    Importance = x.Importance,
                    ReferenceId = x.ReferenceId,
                    Status = x.Status,
                    Type = x.Type,
                    RequestedCount = x.RequestedCount,
                    DelivaryDeadLine = x.DelivaryDeadLine,
                    UnitPrice = x.UnitPrice,
                    TotalPrice = x.TotalPrice,
                    PackingPrice = x.PackingPrice,
                    FinalPrice = x.FinalPrice,
                    Description = x.Description,
                    ManagementDescription = x.ManagementDescription,
                    ContractorId = x.ContractorId,
                    PackageId = x.PackageId,
                    PackageCount = x.PackageCount,
                    PackageUnitPrice = x.PackageUnitPrice,
                    LastDescription = x.LastDescription,
                    Details = x.RequestGoodsSupplyTypeDetails.Select(x => new GetDetailByRGSTypeIdModel
                    {
                        Id = x.Id,
                        CostCenterId = x.CostCenterId,
                        CostCenterName = x.CostCenterId != null ? x.CostCenter!.CostCenterName : null,
                        DeliveryDeadLine = x.DelivaryDeadLine,
                        RequestedCount = x.RequestedCount,
                        Description = x.Description,
                        DescriptionEn = x.DescriptionEn,
                        Urls = x.RequestGoodsSupplyTypeDetailDocuments.Select(x => x.Url).ToList()
                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrRGSModel>? Data, int RowCount)> GetFltrRGS(
    List<long>? projectIds,
    long? cityId,
    long? projectManagerId,
    List<GoodsSupplyStatus>? statuses,
    List<GoodsSupplyType>? types,
    List<long>? creatorIds,
    DateTime? fromDate,
    DateTime? toDate,
    string? filterData,
    int pageIndex,
    int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.IsProjectSupply &&
        x.ProjectId.HasValue && 
        x.Status != GoodsSupplyStatus.Archive).AsQueryable();

        if (projectIds != null)
            query = query.Where(x => projectIds.Contains(x.ProjectId!.Value));

        if (cityId != null)
            query = query.Where(x => x.Project!.CityId == cityId);

        if (projectManagerId != null)
            query = query.Where(x => x.Project!.ProjectManager == projectManagerId);

        if (statuses != null)
            query = query.Where(x => statuses.Contains(x.Status));

        if (types != null)
            query = query.Where(x => types.Contains(x.Type));

        if (creatorIds != null)
            query = query.Where(x => creatorIds.Contains(x.CreatorId));

        if (fromDate != null)
            query = query.Where(x => x.Created >= fromDate);

        if (toDate != null)
            query = query.Where(x => x.Created <= toDate);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            var pattern = filterData.MakeLikePattern();
            query = query.Where(x =>
                EF.Functions.Like(
                    x.SerialNumber.ToString() + "-" + x.Id.ToString(),
                    pattern));
        }

        query = query.OrderByDescending(x => x.Created);

        var newQuery = query.Select(x => new GetFltrRGSModel
        {
            Id = x.Id,
            RequestSerialNumber = x.RequestSerialNumber,
            SerialNumber = x.SerialNumber,
            Status = x.Status,
            Type = x.Type,
            PurchaseLocation = x.PurchaseLocation,
            PurchaseReason = x.PurchaseReason,
            Importance = x.Importance,
            ServiceReasonType = x.ServiceReasonType,
            SupplyerId = x.SupplyerId,
            BuyerId = x.BuyerId,
            CurrencyId = x.CurrencyId,
            TransferPrice = x.TransferPrice,
            OtherPrice = x.OtherPrice,
            DiscountOnInvoicePercentage = x.DiscountOnInvoicePercentage,
            DiscountOnInvoiceNumber = x.DiscountOnInvoiceNumber,
            DiscountedPriceOnInvoice = x.DiscountedPriceOnInvoice,
            TaxOnInvoicePercentage = x.TaxOnInvoicePercentage,
            TaxOnInvoiceNumber = x.TaxOnInvoiceNumber,
            FinalInvoiceAmount = x.FinalInvoiceAmount,
            RequestedDate = x.RequestedDate,
            DeliveryDeadline = x.DeliveryDeadline,
            RegistrationNumber = x.RegistrationNumber,
            RequestingOrganizationId = x.RequestingOrganizationId,
            DescriptionEn = x.DescriptionEn,
            Description = x.Description,
            DeviceName = x.DeviceName,
            DeviceEnName = x.DeviceEnName,
            DeviceNumber = x.DeviceNumber,
            DeviceCode = x.DeviceCode,
            UnitCode = x.UnitCode,
            IsPettyCash = x.IsPettyCash,
            ConsumptionRateAndInventoryUrl = x.ConsumptionRateAndInventoryUrl,
            ConsumptionAddress = x.ConsumptionAddress,
            IsProjectSupply = x.IsProjectSupply,
            ProjectId = x.ProjectId,
            TypeCount = x.RequestGoodsSupplyTypes.Count,
            TypeDetailCount = x.RequestGoodsSupplyTypeDetails.Count,
            Project = x.ProjectId != null ? x.Project!.ProjectName : null,
            CreatorId = x.CreatorId,
            DocumentIds = 
                x.RequestGoodsSupplyDocuments.Select(x => x.Url).ToList()
        });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            newQuery = newQuery.Page(pageIndex, pageSize);

        var baseQuery = await newQuery.ToListAsync(ct);
        return (baseQuery, count);
    }

    public async Task<int> GetLastCodeSerialByPrefix(
    string prefix,
    CT ct)
    {
        var codes = await DbSet
            .Where(x => x.SerialNumber != null && x.SerialNumber.StartsWith(prefix))
            .Select(x => x.SerialNumber!)
            .ToListAsync(ct);

        return codes
            .Select(x => x[prefix.Length..])
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .DefaultIfEmpty(0)
            .Max();
    }
}