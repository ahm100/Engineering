namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;

public record GetRGSTypeByRGSIdRequest(
    long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;