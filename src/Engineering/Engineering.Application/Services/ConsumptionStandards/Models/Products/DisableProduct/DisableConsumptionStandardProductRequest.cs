namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;

public record DisableConsumptionStandardProductRequest(
    long OperationInfoGoodsId
     ) : IHttpRequest;