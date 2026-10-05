using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyManagementRepository : IBaseRepository<RequestGoodsSupplyManagement>
{
    Task<List<RequestGoodsSupplyManagement>> GetRequestGoodsSupplyManagementsAsync(
        List<long>? requestGoodsSupplies,
        List<long>? requestGoodsSupplyDetailIds,
        long? invoiceId,
        CT ct);

    Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByInvoiceId(
        long invoiceId, CT ct);

    Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByConsumableVolumes(
        List<long> consumableVolumeIds, CT ct);

    Task<List<RequestGoodsSupplyManagement>> GetsManagementGoodsSupplyForWarehouse(
        long? invoiceId,
        long? requestGoodsSupplyId,
        string? commercialRequestNo,
        long productId,
        CT ct);

    Task<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>> GetsGoodsSupplyManagmentBySupplyProductId(
        long requestGoodsSupplyProductId,
        CT ct);

    Task<RequestGoodsSupplyManagement?> DoesManagementExist(
        long requestGoodsSupplyProductId,
        long? invoiceId,
        GoodsSupplyManagementType type,
        CT ct);

    Task<List<RequestGoodsSupplyManagement>> GetsSupplyManagementByProjectProducts(
    List<long> projectProductIds,
    CT ct);

    Task<RequestGoodsSupplyManagement?> GetById(
        long id, CT ct);

    Task<(List<RequestGoodsSupplyManagement> Data, int RowCount)> GetByRequestGoodSupplyId(
        long id,
        GoodsSupplyManagementType? type,
        int pageIndex,
        int pageSize,
        CT ct);
}
