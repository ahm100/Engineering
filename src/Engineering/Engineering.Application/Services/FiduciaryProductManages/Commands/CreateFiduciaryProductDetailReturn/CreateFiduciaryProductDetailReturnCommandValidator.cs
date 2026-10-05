namespace Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturn;

public class CreateFiduciaryProductDetailReturnCommandValidator : AbstractValidator<CreateFiduciaryProductDetailReturnCommand>
{
    public CreateFiduciaryProductDetailReturnCommandValidator()
    {
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidCurrencyId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(FiduciaryProductDetailReturnErrors.InValidType);
    }
}
