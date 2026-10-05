namespace Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;

public class CreateFiduciaryProductDetailModelValidator : AbstractValidator<CreateFiduciaryProductDetailModel>
{
    public CreateFiduciaryProductDetailModelValidator()
    {
        RuleFor(oo => oo.ProductId).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductDetailErrors.InValidProductId);
        RuleFor(oo => oo.MeasureUnitId).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductDetailErrors.InValidMeasureUnitId);
        RuleFor(oo => oo.CurrencyId).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductDetailErrors.InValidCurrencyId);
    }
}