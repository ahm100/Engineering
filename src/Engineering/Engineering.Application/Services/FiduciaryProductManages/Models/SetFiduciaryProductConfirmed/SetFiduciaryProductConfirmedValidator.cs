namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;

public class SetFiduciaryProductConfirmedValidator : AbstractValidator<SetFiduciaryProductConfirmedRequest>
{
    public SetFiduciaryProductConfirmedValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
