
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsDestinationWarehouse;

public record GetsDestinationWarehouseResponse(
    List<GetsDestinationWarehouseModel> Data,
    int RowCount
    );
