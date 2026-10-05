
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProductDetailByRequestId;

public record GetsProductDetailByRequestIdRequest(
    long RequestGoodsSupplyId
    ) : IHttpRequest;
