namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;

public class GetFiduciaryProductByIdValidator : AbstractValidator<GetFiduciaryProductByIdRequest>
{
    public GetFiduciaryProductByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
