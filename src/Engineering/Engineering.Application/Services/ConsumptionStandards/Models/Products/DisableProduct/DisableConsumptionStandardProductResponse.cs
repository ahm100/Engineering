namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;

public record DisableConsumptionStandardProductResponse(
    long OperationInfoGoodsId,
    long Id,
    bool isDeleted
    );
