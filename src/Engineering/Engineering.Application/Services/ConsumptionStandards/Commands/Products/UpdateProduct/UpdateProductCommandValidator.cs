namespace Engineering.Application.Services.ConsumptionStandards.Commands.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
        RuleFor(oo => oo.ProductUnitId).NotNull().WithError(ProductStandardErrors.ProductUnitIdIsEmpty);
        RuleFor(oo => oo.Number).GreaterThanOrEqualTo(0).WithError(ProductStandardErrors.ProductNumberIsEmpty);
        RuleFor(oo => oo.StandardProductType)
                .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
                .NotNull().WithError(ProductStandardErrors.TypeIsEmpty);
    }
}