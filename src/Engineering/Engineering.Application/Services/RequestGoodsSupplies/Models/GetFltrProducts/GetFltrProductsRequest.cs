namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;

public record GetFltrProductsRequest(long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;