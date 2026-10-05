namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.CreateFiduciaryProductDetail;

public class CreateFiduciaryProductDetailCommandValidator : AbstractValidator<CreateFiduciaryProductDetailCommand>
{
    public CreateFiduciaryProductDetailCommandValidator()
    {
        RuleFor(oo => oo.ProductId).NotNull().WithError(FiduciaryProductDetailErrors.InValidProductId);
        RuleFor(oo => oo.MeasureUnitId).NotNull().WithError(FiduciaryProductDetailErrors.InValidMeasureUnitId);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailErrors.InValidCurrencyId);
    }
}
