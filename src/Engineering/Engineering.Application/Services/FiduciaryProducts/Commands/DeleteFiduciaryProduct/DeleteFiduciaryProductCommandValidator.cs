namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProduct;

public class DeleteFiduciaryProductCommandValidator : AbstractValidator<DeleteFiduciaryProductCommand>
{
    public DeleteFiduciaryProductCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
