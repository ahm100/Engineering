
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductHistoryById;

public record GetGoodsSupplyProductHistoryByIdRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

