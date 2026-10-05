
namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsSourceWarehouse;

public record GetsSourceWarehouseRequest(
    long RequestGoodsSupplyProductId,
    long? AlternateId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
