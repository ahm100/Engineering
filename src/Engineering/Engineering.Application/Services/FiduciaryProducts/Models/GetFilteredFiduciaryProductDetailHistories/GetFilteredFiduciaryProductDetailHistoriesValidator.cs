namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;

public class GetFilteredFiduciaryProductDetailHistoriesValidator : AbstractValidator<GetFilteredFiduciaryProductDetailHistoriesRequest>
{
    public GetFilteredFiduciaryProductDetailHistoriesValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);
    }
}
