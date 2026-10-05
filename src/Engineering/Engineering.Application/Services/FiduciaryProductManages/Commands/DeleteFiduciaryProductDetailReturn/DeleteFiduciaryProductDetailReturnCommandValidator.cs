namespace Engineering.Application.Services.FiduciaryProductManages.Commands.DeleteFiduciaryProductDetailReturn;

public class DeleteFiduciaryProductDetailReturnCommandValidator : AbstractValidator<DeleteFiduciaryProductDetailReturnCommand>
{
    public DeleteFiduciaryProductDetailReturnCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetailReturn);
    }
}
