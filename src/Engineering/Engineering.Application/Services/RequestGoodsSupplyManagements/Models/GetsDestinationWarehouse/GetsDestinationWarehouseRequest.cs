
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsDestinationWarehouse;

public record GetsDestinationWarehouseRequest(
    long RequestGoodsSupplyDetailId,
    long? AlternateId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
