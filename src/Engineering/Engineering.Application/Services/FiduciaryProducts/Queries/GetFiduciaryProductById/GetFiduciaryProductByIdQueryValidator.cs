namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductById;

public class GetFiduciaryProductByIdQueryValidator : AbstractValidator<GetFiduciaryProductByIdQuery>
{
    public GetFiduciaryProductByIdQueryValidator()
    {
        RuleFor(oo => oo.FiduciaryProductId).NotNull().WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
