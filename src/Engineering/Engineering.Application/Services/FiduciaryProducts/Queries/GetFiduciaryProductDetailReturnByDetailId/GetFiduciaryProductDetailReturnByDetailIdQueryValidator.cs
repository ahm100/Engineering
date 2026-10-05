namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailReturnByDetailId;

public class GetFiduciaryProductDetailReturnByDetailIdQueryValidator : AbstractValidator<GetFiduciaryProductDetailReturnByDetailIdQuery>
{
    public GetFiduciaryProductDetailReturnByDetailIdQueryValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
