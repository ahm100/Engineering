namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;

public class GetFiduciaryProductDetailReturnByDetailIdValidator : AbstractValidator<GetFiduciaryProductDetailReturnByDetailIdRequest>
{
    public GetFiduciaryProductDetailReturnByDetailIdValidator()
    {
        RuleFor(oo => oo.FiduciaryProductDetailId).NotNull().WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
