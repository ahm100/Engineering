namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;

public record GetRequestGoodsDetailHistoryByIdRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

