using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Services;

public record OperationInfoGoodsServiceModel(
    long Id,
    string? GoodsName,
    string? GoodCode,
    decimal GoodsNumber,
    decimal? UnusedPercentage,
    string? MeasurementName,
    StandardProductType StandardProductType,
    ProductAllowedType ProductAllowedType
 );
