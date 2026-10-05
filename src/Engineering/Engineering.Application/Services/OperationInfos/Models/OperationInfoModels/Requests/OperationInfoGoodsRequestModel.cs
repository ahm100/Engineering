using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

public record OperationInfoGoodsRequestModel
{
    public long Id { get; set; }
    public decimal GoodsNumber { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public StandardProductType? StandardProductType { get; set; } = Domain.Entities.OperationInfos.Enums.StandardProductType.ProductGroup;
    public ProductAllowedType? ProductAllowedType { get; set; } = Domain.Entities.OperationInfos.Enums.ProductAllowedType.IsStandard;
}
