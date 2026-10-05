namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductPending;

public class SetFiduciaryProductPendingValidator : AbstractValidator<SetFiduciaryProductPendingRequest>
{
    public SetFiduciaryProductPendingValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
