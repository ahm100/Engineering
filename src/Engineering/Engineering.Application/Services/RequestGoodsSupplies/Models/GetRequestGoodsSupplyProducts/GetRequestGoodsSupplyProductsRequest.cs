namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? RequestGoodsSupplyIds,
    List<long>? ProductGroupIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
