namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;

public record GetGoodsSupplyProductByIdRequest(
    long Id
    ) : IHttpRequest;
