namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;

public record GetRequestGoodsHistoryByIdRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

