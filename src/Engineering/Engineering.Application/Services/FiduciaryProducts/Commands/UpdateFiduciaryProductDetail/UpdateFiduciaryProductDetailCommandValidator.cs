namespace Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateFiduciaryProductDetail;

public class UpdateFiduciaryProductDetailCommandValidator : AbstractValidator<UpdateFiduciaryProductDetailCommand>
{
    public UpdateFiduciaryProductDetailCommandValidator()
    {
        RuleFor(oo => oo.ProductId).NotNull().WithError(FiduciaryProductDetailErrors.InValidProductId);
        RuleFor(oo => oo.MeasureUnitId).NotNull().WithError(FiduciaryProductDetailErrors.InValidMeasureUnitId);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailErrors.InValidCurrencyId);
        RuleFor(oo => oo.FiduciaryProductDetail).NotEmpty().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
