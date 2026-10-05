
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;

public record GetsManagementGoodsSupplyForWarehouseRequest(
    long? InvoiceId,
    long? RequestGoodsSupplyId,
    string? CommercialRequestNo,
    long ProductId
    ) : IHttpRequest;
