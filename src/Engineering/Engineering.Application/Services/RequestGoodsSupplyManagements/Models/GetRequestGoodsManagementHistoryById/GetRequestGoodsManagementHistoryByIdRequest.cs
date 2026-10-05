namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsManagementHistoryById;

public record GetRequestGoodsManagementHistoryByIdRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

