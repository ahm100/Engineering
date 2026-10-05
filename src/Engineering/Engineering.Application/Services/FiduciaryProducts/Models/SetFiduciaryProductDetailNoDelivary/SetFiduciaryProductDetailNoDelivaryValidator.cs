namespace Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailNoDelivary;

public class SetFiduciaryProductDetailNoDelivaryValidator : AbstractValidator<SetFiduciaryProductDetailNoDelivaryRequest>
{
    public SetFiduciaryProductDetailNoDelivaryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

    }
}

