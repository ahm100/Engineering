using Engineering.Domain.Entities.OperationInfos.Enums;
using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.UpdateProduct;

public record UpdateProductCommand(
    long Id,
    long ProductUnitId,
    decimal Number,
    decimal? UnusedPercentage,
    StandardProductType StandardProductType,
    ProductAllowedType ProductAllowedType
    ) : ICommand<ConsumptionStandardProduct>;