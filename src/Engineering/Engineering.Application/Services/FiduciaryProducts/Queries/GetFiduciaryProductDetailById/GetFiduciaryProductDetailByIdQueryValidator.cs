namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailById;

public class GetFiduciaryProductDetailByIdQueryValidator : AbstractValidator<GetFiduciaryProductDetailByIdQuery>
{
    public GetFiduciaryProductDetailByIdQueryValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
