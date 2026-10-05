using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsManagementGoodsSupplyForWarehouse;

public record GetsManagementGoodsSupplyForWarehouseQuery(
    long? InvoiceId,
    long? RequestGoodsSupplyId,
    string? CommercialRequestNo,
    long ProductId
    ) : IQuery<List<RequestGoodsSupplyManagement>>;
