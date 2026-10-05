namespace Engineering.Application.Services.FiduciaryProducts.Models.DeleteFiduciaryProduct;

public class DeleteFiduciaryProductValidator : AbstractValidator<DeleteFiduciaryProductRequest>
{
    public DeleteFiduciaryProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
