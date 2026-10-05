namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;

public record GetsManagementGoodsSupplyForWarehouseResponse(
    List<GetsManagementGoodsSupplyForWarehouseModel> Data,
    int RowCount
    );
