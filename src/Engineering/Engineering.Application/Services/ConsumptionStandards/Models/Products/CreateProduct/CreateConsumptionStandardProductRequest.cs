using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.CreateProduct;

public record CreateConsumptionStandardProductRequest : IHttpRequest
{
    public long Id { get; set; }
    public decimal GoodsNumber { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public long OperationInfoId { get; set; }
    public StandardProductType? StandardProductType { get; set; } = Domain.Entities.OperationInfos.Enums.StandardProductType.ProductGroup;
    public ProductAllowedType? ProductAllowedType { get; set; } = Domain.Entities.OperationInfos.Enums.ProductAllowedType.IsStandard;
}
