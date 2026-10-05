namespace Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProductDetail;

public class DeleteFiduciaryProductDetailCommandValidator : AbstractValidator<DeleteFiduciaryProductDetailCommand>
{
    public DeleteFiduciaryProductDetailCommandValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
