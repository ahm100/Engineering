namespace Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

public class UpdateFiduciaryProductDetailModelValidator : AbstractValidator<UpdateFiduciaryProductDetailModel>
{
    public UpdateFiduciaryProductDetailModelValidator()
    {
        RuleFor(oo => oo.ProductId).NotNull().WithError(FiduciaryProductDetailErrors.InValidProductId);
        RuleFor(oo => oo.MeasureUnitId).NotNull().WithError(FiduciaryProductDetailErrors.InValidMeasureUnitId);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailErrors.InValidCurrencyId);
    }
}