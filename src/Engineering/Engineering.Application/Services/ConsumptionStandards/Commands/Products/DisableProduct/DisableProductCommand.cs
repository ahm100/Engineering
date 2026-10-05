using ConsumptionStandardProduct = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardProduct;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.DisableProduct;

public record DisableProductCommand(
    long Id
    ) : ICommand<ConsumptionStandardProduct>;