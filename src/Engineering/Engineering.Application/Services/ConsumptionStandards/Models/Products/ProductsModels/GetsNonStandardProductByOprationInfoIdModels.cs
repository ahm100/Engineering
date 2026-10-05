using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.ProducModels;

public record GetsNonStandardProductByOprationInfoIdModels(
    long? OperationInfoGoodsId,
    long Id,
    string? Name,
    string? Code,
    bool? IsActive,
    long? MeasureUnitId,
    string? MeasureUnitName,
    decimal? GoodsNumber,
    decimal? UnusedPercentage,
    StandardProductType StandardProductType,
    string TypeDescription
    );
