namespace Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailDelivary;

public class SetFiduciaryProductDetailDelivaryValidator : AbstractValidator<SetFiduciaryProductDetailDelivaryRequest>
{
    public SetFiduciaryProductDetailDelivaryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
