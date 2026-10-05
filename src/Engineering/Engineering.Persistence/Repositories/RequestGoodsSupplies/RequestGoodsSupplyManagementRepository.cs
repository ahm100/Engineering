using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class RequestGoodsSupplyManagementRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyManagement>, IRequestGoodsSupplyManagementRepository
{
    public RequestGoodsSupplyManagementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestGoodsSupplyManagement?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestGoodsSupplyProduct!.RequestGoodsSupplyManagements)
            .Include(oo => oo.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestGoodsSupplyProduct!)
                .ThenInclude(oo => oo.RequestGoodsSupplyDetails)
                    .ThenInclude(oo => oo.ConsumableVolumeProduct.ProjectOperationDetail)

            .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<RequestGoodsSupplyManagement> Data, int RowCount)> GetByRequestGoodSupplyId(long id, GoodsSupplyManagementType? type, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(c => c.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.OperationInfo)
            .Include(c => c.RequestGoodsSupplyProduct!.RequestGoodsSupplyDetails)
                .ThenInclude(c => c.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation)

            .Where(c => c.RequestGoodsSupplyProduct!.Id == id &&
                    (type == null || c.Type == type));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<RequestGoodsSupplyManagement>> GetRequestGoodsSupplyManagementsAsync(List<long>? requestGoodsSupplies, List<long>? requestGoodsSupplyDetailIds, long? invoiceId, CT ct)
    {
        var query = DbSet
                         .Include(c => c.RequestGoodsSupplyProduct)
                            .ThenInclude(c => c!.RequestGoodsSupply)

                         .Where(c => (requestGoodsSupplies == null || requestGoodsSupplies.Contains(c.RequestGoodsSupplyProduct!.RequestGoodsSupply.Id)) &&
                                     (requestGoodsSupplyDetailIds == null || requestGoodsSupplyDetailIds.Contains(c.RequestGoodsSupplyProduct!.Id)) &&
                                     (invoiceId == null || c.InvoiceId == invoiceId))
            .OrderByDescending(oo => oo.Created);


        return await query.ToListAsync(ct);
    }

    public async Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByInvoiceId(long invoiceId, CT ct)
    {
        var query = DbSet
                         .Include(c => c.RequestGoodsSupplyProduct)
                         .Where(c => c.InvoiceId == invoiceId &&
                         (c.Status == GoodsSupplyManagementStatus.Pending ||
                            c.Status == GoodsSupplyManagementStatus.Return))
            .OrderByDescending(oo => oo.Created);


        return await query.ToListAsync(ct);
    }

    public async Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByConsumableVolumes(
    List<long> consumableVolumeIds,
    CT ct)
    {
        var query = DbSet
            .Where(c =>
                !c.IsDeleted &&
                c.Status != GoodsSupplyManagementStatus.Return &&
                c.RequestGoodsSupplyProduct != null &&
                c.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.Any(x =>
                    x.ConsumableVolumeProduct != null &&
                    consumableVolumeIds.Contains(x.ConsumableVolumeProduct.Id))
            );

        return await query.ToListAsync(ct);
    }

    public async Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByProjectProducts(
    List<long> projectProductIds,
    CT ct)
    {
        var query = DbSet
            .Where(c =>
                !c.IsDeleted &&
                c.Status != GoodsSupplyManagementStatus.Return &&
                c.RequestGoodsSupplyProduct != null &&
                c.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.Any(x =>
                    x.ProjectProductId.HasValue &&
                    projectProductIds.Contains(x.ProjectProductId.Value))
            );

        return await query.ToListAsync(ct);
    }

    public async Task<List<RequestGoodsSupplyManagement>> GetsManagementGoodsSupplyForWarehouse(
        long? invoiceId,
        long? requestGoodsSupplyId,
        string? commercialRequestNo,
        long productId, CT ct)
    {
        var query = DbSet
            .Include(c => c.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Include(c => c.RequestGoodsSupplyProduct)
                .ThenInclude(c => c!.RequestGoodsSupplyDetails)
                    .ThenInclude(c => c.ConsumableVolumeProduct.ProjectOperationDetail)
            .Include(c => c.RequestGoodsSupplyProduct)
                .ThenInclude(c => c!.RequestGoodsSupplyDetails)
                    .ThenInclude(c => c!.ProjectProduct)
            .Include(c => c.RequestGoodsSupplyProduct!.RequestGoodsSupply.Project.ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)
            .Where(c =>
               (invoiceId == null || c.InvoiceId == invoiceId) &&
               (requestGoodsSupplyId == null || c.RequestGoodsSupplyProduct!.Id == requestGoodsSupplyId) &&
               (string.IsNullOrWhiteSpace(commercialRequestNo) ||
               EF.Functions.Like(c.RequestGoodsSupplyProduct!.SerialNumber.ToString() + "-" + c.RequestGoodsSupplyProduct.Id.ToString(), commercialRequestNo.MakeLikePattern())) &&
               c.ReferenceId == productId &&
               (c.Status != GoodsSupplyManagementStatus.Return))

            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>> GetsGoodsSupplyManagmentBySupplyProductId(
        long requestGoodsSupplyProductId, CT ct)
    {
        var query = DbSet

            .Where(c => c.RequestGoodsSupplyProduct!.Id == requestGoodsSupplyProductId)

            .Select(item => new GetsGoodsSupplyManagmentBySupplyProductIdModel()
            {
                Id = item.Id,
                Type = item.Type,
                Status = item.Status,
                InvoiceId = item.InvoiceId,
                WarehouseId = item.WarehouseId,
                DestinationWarehouseId = item.DestinationWarehouseId,
                ProductId = item.ReferenceId,
                RequestedCount = item.RequestedCount,
                ConfirmedRequestCount = item.ConfirmedRequestCount,
                AlternateId = item.AlternateId,
                OperatorAppointmentId = item.OperatorAppointmentId,
                Description = item.Description,
                LastDescription = item.LastDescription,
                AssignmentDate = item.AssignmentDate,
            });

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<RequestGoodsSupplyManagement?> DoesManagementExist(
        long requestGoodsSupplyProductId,
        long? invoiceId,
        GoodsSupplyManagementType type,
        CT ct)
    {
        var query = DbSet
            .Where(c => c.RequestGoodsSupplyProduct!.Id == requestGoodsSupplyProductId
            && c.Type == type && invoiceId == c.InvoiceId);
        return await query.FirstOrDefaultAsync(ct);
    }


}
