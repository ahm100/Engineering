namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredAlternativeProducts;

public record GetFilteredAlternativeProductsRequest(
    long RequestGoodsSupplyDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
