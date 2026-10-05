namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;

public record GetDetailByRGSIdRequest(
    long Id,
    int PageIndex,
    int PageSize) : IHttpRequest;