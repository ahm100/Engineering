namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(oo => oo.ProductUnitId).NotNull().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
        RuleFor(oo => oo.StandardProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(ProductStandardErrors.TypeIsEmpty);
        RuleFor(oo => oo.Number).GreaterThanOrEqualTo(0).WithError(ProductStandardErrors.ProductNumberIsEmpty);
    }
}