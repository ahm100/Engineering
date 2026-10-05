namespace Engineering.Application.Services.FiduciaryProducts.Commands.SetFiduciaryProductDetailDelivary;

public class SetFiduciaryProductDetailDelivaryCommandValidator : AbstractValidator<SetFiduciaryProductDetailDelivaryCommand>
{
    public SetFiduciaryProductDetailDelivaryCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

    }
}
