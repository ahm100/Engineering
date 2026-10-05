using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyDetailDocumentRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyDetailDocument>, IRequestGoodsSupplyDetailDocumentRepository
{
    public RequestGoodsSupplyDetailDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<string>> GetGoodsSupplyProductDocuments(
        long id, CT ct)
    {
        var query = DbSet.

            Where(x => x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.Id.Equals(id))

            .Select(x => x.Url).Distinct();

        var items = await query.ToListAsync(ct);

        return (items);
    }

    public async Task<List<GetAllGoodsSupplyProductDocumentResponseModel>> GetAllGoodsSupplyProductDocument(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        long? cityId,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetail)
               .ThenInclude(oo => oo.RequestGoodsSupplyProduct)

            .Where(x =>
                !x.RequestGoodsSupplyDetail.RequestGoodsSupply.IsDeleted &&
                !x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.IsDeleted &&
                !x.RequestGoodsSupplyDetail.IsDeleted &&
                !x.IsDeleted &&
                (ids == null || ids.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.Id)) &&
                (costCenterIds == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                (cityId == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                (projectIds == null || projectIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
                (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Id)) &&
                (projectOperationDetailIds == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupplyDetails.Any(d => projectOperationDetailIds.Contains(d.ConsumableVolumeProduct.ProjectOperationDetail.Id))) &&
                (productIds == null || productIds.Count == 0 || productIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.ProductId)) &&
                (startDate == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.Created >= startDate) &&
                (endDate == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.Created.Date <= endDate.Value.Date) &&
                (types == null || types.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.RequestGoodsSupply.Type)) &&
                (statuses == null || statuses.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct!.Status))
                )
            .GroupBy(c => c.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct)
            .Select(d => new GetAllGoodsSupplyProductDocumentResponseModel()
            {
                Id = d.Key!.Id,
                Documents = d.Select(c => c.Url).ToList(),
            });

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items);
    }

    public async Task<List<RequestGoodsSupplyDetailDocument>?> GetRequestGoodsSupplyProductByIds(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string? filterProduct,
        DateTime? fromDate,
        DateTime? toDate,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyDetail)
               .ThenInclude(oo => oo.RequestGoodsSupplyProduct)

             .Where(x =>
                   !x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.IsDeleted &&
                   !x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.IsDeleted &&
                  (ids == null || ids.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Id)) &&
                  (costCenterIds == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                  (cityId == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenter.CityId == cityId)) &&
                  (projectIds == null || projectIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project.Id)) &&
                  (projectManagerId == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project.ProjectManager.Equals(projectManagerId)) &&
                  (projectOperationIds == null || projectOperationIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Id)) &&
                  (projectOperationDetailIds == null || projectOperationDetailIds.Contains(x.RequestGoodsSupplyDetail.ConsumableVolumeProduct.ProjectOperationDetail.Id)) &&
                  (productIds == null || productIds.Count == 0 || productIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.ProductId)) &&
                  (creatorIds == null || creatorIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.CreatorId)) &&
                  (warehouseIds == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.Type == GoodsSupplyType.Project && warehouseIds.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.DestinationWarehouseId!.Value) ||
                   x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupplyManagements.Any(m => m.WarehouseId.HasValue && warehouseIds.Contains(m.WarehouseId.Value)) ||
                   x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupplyManagements.Any(m => m.DestinationWarehouseId.HasValue && warehouseIds.Contains(m.DestinationWarehouseId.Value))) &&
                  (fromDate == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Created.Date >= fromDate.Value.Date) &&
                  (toDate == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Created.Date <= toDate.Value.Date) &&
                  (types == null || types.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.Type)) &&
                  (statuses == null || statuses.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Status)) &&
                  (removeStatuses == null || !removeStatuses.Contains(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Status)) &&
                  (customerInvoiceNumber == null || x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.CustomerInvoiceNumber == customerInvoiceNumber) &&
                  (string.IsNullOrWhiteSpace(filterManagerDescription) || EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.ManagementDescription, filterManagerDescription.MakeLikePattern())) &&
                  (string.IsNullOrWhiteSpace(filterOperationInfoName) || EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.ProjectOperation.OperationInfo.OperationInfoName, filterOperationInfoName.MakeLikePattern())) &&
                  (string.IsNullOrWhiteSpace(filterPublicName) || EF.Functions.Like(x.RequestGoodsSupplyDetail.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName, filterPublicName.MakeLikePattern())) &&
                  (string.IsNullOrWhiteSpace(filterDescription) || EF.Functions.Like(x.RequestGoodsSupplyDetail.ConsumableVolumeProduct.ProjectOperationDetail.Description, filterDescription.MakeLikePattern())) &&
                  (string.IsNullOrWhiteSpace(requestNumber) || EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.SerialNumber.ToString() + "-" + x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.RequestGoodsSupply.Id.ToString(), requestNumber.MakeLikePattern())) &&
                  (string.IsNullOrWhiteSpace(filterData) ||
                   EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.SerialNumber.ToString() + "-" + x.Id.ToString(), filterData.MakeLikePattern()) ||
                   EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.ManagementDescription, filterData.MakeLikePattern()) ||
                   EF.Functions.Like(x.RequestGoodsSupplyDetail.RequestGoodsSupplyProduct.Description, filterData.MakeLikePattern()))
                  );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var items = await query.ToListAsync(ct);
        return items;
    }
}
