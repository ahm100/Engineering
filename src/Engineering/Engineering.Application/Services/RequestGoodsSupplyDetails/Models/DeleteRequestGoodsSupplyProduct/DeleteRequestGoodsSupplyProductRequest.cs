using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyProduct;

public record DeleteRequestGoodsSupplyProductRequest(
    long? Id
    ) : IHttpRequest;

public record DeleteRequestGoodsSupplyProductModelRequest(
    long? Id,
    RequestGoodsSupplyProduct? RequestGoodsSupplyProduct,
    bool CheckAnotherData
    );
