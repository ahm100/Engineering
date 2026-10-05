namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductRejected;

public class SetFiduciaryProductRejectedValidator : AbstractValidator<SetFiduciaryProductRejectedRequest>
{
    public SetFiduciaryProductRejectedValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
