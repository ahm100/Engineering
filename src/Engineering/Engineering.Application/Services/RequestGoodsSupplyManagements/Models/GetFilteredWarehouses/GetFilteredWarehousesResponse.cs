
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsSourceWarehouse;

public record GetsSourceWarehouseResponse(
    List<GetsSourceWarehouseModel> Data,
    int RowCount
    );
