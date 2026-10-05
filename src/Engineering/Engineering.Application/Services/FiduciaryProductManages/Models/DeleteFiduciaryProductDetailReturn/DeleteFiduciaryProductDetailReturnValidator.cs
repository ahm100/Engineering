namespace Engineering.Application.Services.FiduciaryProductManages.Models.DeleteFiduciaryProductDetailReturn;

public class DeleteFiduciaryProductDetailReturnValidator : AbstractValidator<DeleteFiduciaryProductDetailReturnRequest>
{
    public DeleteFiduciaryProductDetailReturnValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetailReturn);
    }
}
