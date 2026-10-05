using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;

public record OperationInfoGoodsDataModel(
    long OperationInfoGoodsId,
    long Id,
    string? Name,
    string? Code,
    bool? IsAcive,
    decimal GoodsNumber,
    decimal? UnusedPercentage,
    long? MeasureUnitId,
    string? MeasureUnitName,
    StandardProductType StandardProductType,
    string TypeDescription,
    ProductAllowedType ProductAllowedType,
    string ProductAllowedTypeDescription
 );
