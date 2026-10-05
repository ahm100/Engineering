namespace Engineering.Application.Services.FiduciaryProductManages.Models.UpdateFiduciaryProductDetailReturn;

public class UpdateFiduciaryProductDetailReturnValidator : AbstractValidator<UpdateFiduciaryProductDetailReturnRequest>
{
    public UpdateFiduciaryProductDetailReturnValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetailReturn);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidCurrencyId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(FiduciaryProductDetailReturnErrors.InValidType);
    }
}
