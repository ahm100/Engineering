using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.UpdateProduct;

public record UpdateConsumptionStandardProductRequest : IHttpRequest
{
    public long OperationInfoGoodsId { get; set; }
    public long Id { get; set; }
    public decimal GoodsNumber { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public StandardProductType? StandardProductType { get; set; } = Domain.Entities.OperationInfos.Enums.StandardProductType.ProductGroup;
    public ProductAllowedType? ProductAllowedType { get; set; } = Domain.Entities.OperationInfos.Enums.ProductAllowedType.IsStandard;
}

