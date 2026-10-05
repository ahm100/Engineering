namespace Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductDetailReturnById;

public class GetFiduciaryProductDetailReturnByIdQueryValidator : AbstractValidator<GetFiduciaryProductDetailReturnByIdQuery>
{
    public GetFiduciaryProductDetailReturnByIdQueryValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailReturnId).NotNull().WithError(FiduciaryProductDetailReturnErrors.InValidFiduciaryProductDetailReturn);
    }
}
