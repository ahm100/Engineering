namespace Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;

public class CreateFiduciaryProductDetailReturnModelRequestValidator : AbstractValidator<CreateFiduciaryProductDetailReturnModelRequest>
{
    public CreateFiduciaryProductDetailReturnModelRequestValidator()
    {
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidCurrencyId);
        RuleFor(oo => oo.Type).IsInEnum().WithError(FiduciaryProductDetailReturnErrors.InValidType);
        RuleFor(oo => oo.ReturnCount).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidReturnCount);
        RuleFor(oo => oo.ReturnDate).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidReturnDate);
        RuleFor(oo => oo.LateDay).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidLateDay);
        RuleFor(oo => oo.LateFine).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidLateFine);
        RuleFor(oo => oo.LateFine).LessThan(9999999999999999).WithError(FiduciaryProductDetailReturnErrors.LateFineCanNotGreater);
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetail);
    }
}