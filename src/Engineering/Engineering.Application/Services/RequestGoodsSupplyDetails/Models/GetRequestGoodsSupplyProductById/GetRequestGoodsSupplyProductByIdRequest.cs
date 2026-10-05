namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;

public record GetRequestGoodsSupplyProductByIdRequest(
    long Id
    ) : IHttpRequest;
