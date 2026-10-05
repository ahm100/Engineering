using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProductIds;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public partial class RequestGoodsSupplyDetailRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyDetail>, IRequestGoodsSupplyDetailRepository
{
    public RequestGoodsSupplyDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetail(long id, List<GoodsSupplyDetailStatus>? statuses, List<GoodsSupplyDetailStatus>? removeStatuses,
         string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyManagements)
            .Include(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(c => c.ConsumableVolumeProduct)
                .ThenInclude(c => c.ProjectOperationDetail)
                    .ThenInclude(c => c.OperationLocation)

            .Where(c =>
                c.RequestGoodsSupply.Id.Equals(id) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsGoodsSupplyDetailByProductId(long id, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyManagements)
            .Include(oo => oo.RequestGoodsSupplyDetailDocuments)
            .Include(c => c.ConsumableVolumeProduct)
                .ThenInclude(c => c.ProjectOperationDetail)
                    .ThenInclude(c => c.OperationLocation)

            .Where(c =>
                c.RequestGoodsSupplyProduct!.Id.Equals(id));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsGoodsSupplyDetailByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(c => c.ConsumableVolumeProduct)

            .Where(c =>
                c.ConsumableVolumeProduct.ProjectOperationDetail.ProjectOperation!.Id.Equals(projectOperationId));

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetFilteredAlternativeProducts(List<long>? requestIds, List<long>? products, List<long>? requestGoodsSupplyDetailIds, CT ct)
    {
        var query = DbSet.Include(c => c.ConsumableVolumeProduct)
                            .ThenInclude(c => c.ProjectOperationDetail)
                                .ThenInclude(c => c.OperationLocation)
                          .Include(oo => oo.RequestGoodsSupply)
                            .ThenInclude(oo => oo.ProjectOperation)
                                .ThenInclude(oo => oo.Project)
                                    .ThenInclude(oo => oo.ProjectCostCenters)
                                        .ThenInclude(oo => oo.CostCenter)
                          .Include(oo => oo.RequestGoodsSupplyManagements)

                         .Where(c => (requestIds == null || requestIds.Contains(c.RequestGoodsSupply.Id)) &&
                                     (products == null || products.Contains(c.ProductId)) &&
                                     (requestGoodsSupplyDetailIds == null || requestGoodsSupplyDetailIds.Contains(c.Id)))
        .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetailByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyManagements)
            .Include(oo => oo.ProjectProduct)
            .Where(c => ids.Contains(c.Id) && !c.IsDeleted)

            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<RequestGoodsSupplyDetail?> GetRequestGoodsSupplyDetailById(long id, CT ct)
    {
        var query = DbSet
            .Where(c => c.Id.Equals(id) && !c.IsDeleted)

            .Include(oo => oo.RequestGoodsSupplyProduct)
            .Include(oo => oo.RequestGoodsSupply)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetails)
            .Include(oo => oo.ConsumableVolumeProduct)
                .ThenInclude(oo => oo.ProjectOperationDetail)
                    .ThenInclude(oo => oo.ProjectOperation.Project.ProjectCostCenters)
                        .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ConsumableVolumeProduct)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetails)
                    .ThenInclude(x => x.RequestGoodsSupplyManagements)
            .Include(oo => oo.RequestGoodsSupplyDetailDocuments);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> RequestGoodsSupplyDetailsByRequestIdAsync(long requestId, CT ct)
    {
        var query = DbSet.Where(c => c.RequestGoodsSupply.Id.Equals(requestId) &&
                                     !c.IsDeleted)
            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<RequestGoodsSupplyDetail> GetRequestGoodSupply(
        long goodsSupplyProductId, CT ct)
    {
        var query = DbSet
            .Include(x => x.RequestGoodsSupply)
            .FirstOrDefault(c => c.RequestGoodsSupplyProduct!.Id == goodsSupplyProductId);

        return query;
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetailForDaily(long projectOperationDetailId, CT ct)
    {
        var query = DbSet
            .Include(c => c.ConsumableVolumeProduct)
                .ThenInclude(c => c.ProjectOperationDetail)
            .Include(oo => oo.RequestGoodsSupplyManagements)

            .Where(c => c.ConsumableVolumeProduct.ProjectOperationDetail.Id.Equals(projectOperationDetailId) &&
                        c.RequestGoodsSupplyProduct!.RequestGoodsSupplyManagements.Any(x => x.Status == GoodsSupplyManagementStatus.CompleteSupply) &&
                        !c.IsDeleted)

            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<GetTotalSupplyByProductIdsModel>> GetTotalSupplyByProductIdsAsync(List<long>? products, CT ct)
    {
        return await DbSet.Where(c =>
                                 (products == null || products.Contains(c.ProductId)) &&
                                      !c.IsDeleted)
                          .OrderByDescending(oo => oo.Created)
                          .GroupBy(c => c.ProductId)
                          .Select(c => new GetTotalSupplyByProductIdsModel
                          {
                              ProductId = c.Key,
                              TotalSupplyCount = c.Sum(s => s.RequestedCount)
                          })
                          .ToListAsync(ct);
    }

    public async Task<List<GetsGoodsSupplyDetailBySupplyProductIdModel>> GetsGoodsSupplyDetailBySupplyProductId(
        long goodsSupplyProductId,
        CT ct)
    {
        var query = DbSet

            .Where(c => c.RequestGoodsSupplyProduct!.Id == goodsSupplyProductId)

            .Select(detail => new GetsGoodsSupplyDetailBySupplyProductIdModel()
            {
                RequestGoodsSupplyId = detail.RequestGoodsSupply.Id,
                ProjectOperationId = detail.RequestGoodsSupply.ProjectOperation.Id,
                Id = detail.Id,
                ProductId = detail.ProductId,
                ProjectOperationDetailId = detail.ConsumableVolumeProduct.ProjectOperationDetail.Id,
                ConsumableVolumeProductId = detail.ConsumableVolumeProduct.Id,
                ProductNumber = detail.ConsumableVolumeProduct.FinalValue,
                ProductGroupId = detail.ConsumableVolumeProduct.ProductGroupId,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                RequestedCount = (float)detail.RequestedCount,
                UnitPrice = detail.UnitPrice,
                PackageId = detail.PackageId,
                TotalPrice = detail.TotalPrice,
                DiscountByNumber = detail.DiscountByNumber,
                DiscountByPercentage = detail.DiscountByPercentage,
                DiscountedPrice = detail.DiscountedPrice,
                TaxNumber = detail.TaxNumber,
                TaxPercentage = detail.TaxPercentage,
                PackingPrice = detail.PackingPrice,
                FinalPrice = detail.FinalPrice,
                PackageCount = detail.PackageCount,
                Description = detail.Description,
                ManagementDescription = detail.ManagementDescription,
                CustomerInvoiceNumber = detail.CustomerInvoiceNumber,
                LastDescription = detail.LastDescription
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<List<GetsGoodsSupplyDetailBySupplyProductIdModel>> GetsProjectGoodsSupplyDetailBySupplyProductId(
        long goodsSupplyProductId,
        CT ct)
    {
        var query = DbSet

            .Where(c => c.RequestGoodsSupplyProduct!.Id == goodsSupplyProductId)

            .Select(detail => new GetsGoodsSupplyDetailBySupplyProductIdModel()
            {
                Id = detail.Id,
                RequestGoodsSupplyId = detail.RequestGoodsSupply.Id,
                ProjectId = detail.RequestGoodsSupply.ProjectId,
                ProductId = detail.ProductId,
                ProjectProductId = detail.ProjectProduct.Id,
                ProductNumber = detail.ProjectProduct.RequestQuantity,
                ProductGroupId = detail.ProjectProduct.ProductGroupId,
                ProductCategoryId = detail.ProjectProduct.ProductCategoryId,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                RequestedCount = (float)detail.RequestedCount,
                UnitPrice = detail.UnitPrice,
                PackageId = detail.PackageId,
                TotalPrice = detail.TotalPrice,
                DiscountByNumber = detail.DiscountByNumber,
                DiscountByPercentage = detail.DiscountByPercentage,
                DiscountedPrice = detail.DiscountedPrice,
                TaxNumber = detail.TaxNumber,
                TaxPercentage = detail.TaxPercentage,
                PackingPrice = detail.PackingPrice,
                FinalPrice = detail.FinalPrice,
                PackageCount = detail.PackageCount,
                Description = detail.Description,
                ManagementDescription = detail.ManagementDescription,
                CustomerInvoiceNumber = detail.CustomerInvoiceNumber,
                LastDescription = detail.LastDescription
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsProductDetailByRequestId(
        long requestGoodsSupplyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ConsumableVolumeProduct)
                .ThenInclude(c => c.ProjectOperationDetail)
                    .ThenInclude(x => x.OperationLocation)
            .Include(oo => oo.RequestGoodsSupplyManagements)

            .Where(c => c.RequestGoodsSupply.Id == requestGoodsSupplyId)

            .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetRequestGoodsSupplyDetailsForExcel(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types,
        List<long>? creatorIds,
        List<long>? productIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
           .Include(oo => oo.RequestGoodsSupply.ProjectOperation.OperationInfo)
           .Include(oo => oo.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
           .Include(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)
           .Include(oo => oo.RequestGoodsSupplyManagements)

           .Where(c =>
               (companyId == null || c.RequestGoodsSupply.CompanyId == companyId) &&
               (productIds == null || productIds.Count == 0 || productIds.Contains(c.ProductId)) &&
               (creatorIds == null || creatorIds.Count == 0 || creatorIds.Contains(c.RequestGoodsSupply.CreatorId)) &&
               (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
               (costCenterIds == null || c.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
               (projectIds == null || projectIds.Contains(c.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
               (cityId == null || c.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
               (projectManagerId == null || c.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
               (projectOperationIds == null || projectOperationIds.Contains(c.RequestGoodsSupply.ProjectOperation.Id)) &&
               (types == null || types.Contains(c.RequestGoodsSupply.Type)) &&
               (statuses == null || statuses.Contains(c.RequestGoodsSupply.Status)) &&
               (removeStatuses == null || !removeStatuses.Contains(c.RequestGoodsSupply.Status)) &&
               (fromDate == null || c.Created.Date >= fromDate.Value.Date) &&
               (toDate == null || c.Created.Date <= toDate.Value.Date) &&
               (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.RequestGoodsSupply.SerialNumber.ToString() + "-" + c.RequestGoodsSupply.Id.ToString(), filterData.MakeLikePattern())) &&
               (projectOperationDetailIds == null || projectOperationDetailIds.Contains(c.ConsumableVolumeProduct.ProjectOperationDetail.Id) ||
                projectOperationDetailIds.Contains(c.RequestGoodsSupply.ProjectOperationDetail.Id))
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

    public async Task<decimal> GetDailyRequestProductsCount(
    CT ct)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return await DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.RequestGoodsSupply.RequestedDate >= today &&
                x.RequestGoodsSupply.RequestedDate < tomorrow)
            .SumAsync(x => x.RequestedCount, ct);
    }

    public async Task<decimal> GetDailyRequestProvidedProductsCount(
    CT ct)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return await DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.Status == GoodsSupplyDetailStatus.CompleteSupply &&
                x.Updated.HasValue &&
                x.Updated.Value >= today &&
                x.Updated.Value < tomorrow)
            .SumAsync(x => x.RequestedCount, ct);
    }
}
