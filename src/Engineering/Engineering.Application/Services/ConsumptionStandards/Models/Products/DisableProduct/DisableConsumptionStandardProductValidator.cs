namespace Engineering.Application.Services.ConsumptionStandards.Models.Products.DisableProduct;

public class DisableConsumptionStandardProductValidator : AbstractValidator<DisableConsumptionStandardProductRequest>
{
    public DisableConsumptionStandardProductValidator()
    {
        RuleFor(oo => oo.OperationInfoGoodsId).NotNull().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
    }
}
