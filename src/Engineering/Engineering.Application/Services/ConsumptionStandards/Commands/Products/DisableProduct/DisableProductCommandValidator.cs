
namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.DisableProduct;

public class DisableProductCommandValidator : AbstractValidator<DisableProductCommand>
{
    public DisableProductCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
    }
}