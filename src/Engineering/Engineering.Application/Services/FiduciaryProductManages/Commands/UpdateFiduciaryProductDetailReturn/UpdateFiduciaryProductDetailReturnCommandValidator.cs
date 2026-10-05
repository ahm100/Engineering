namespace Engineering.Application.Services.FiduciaryProductManages.Commands.UpdateFiduciaryProductDetailReturn;

public class UpdateFiduciaryProductDetailReturnCommandValidator : AbstractValidator<UpdateFiduciaryProductDetailReturnCommand>
{
    public UpdateFiduciaryProductDetailReturnCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetailReturn);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidCurrencyId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(FiduciaryProductDetailReturnErrors.InValidType);
    }
}
