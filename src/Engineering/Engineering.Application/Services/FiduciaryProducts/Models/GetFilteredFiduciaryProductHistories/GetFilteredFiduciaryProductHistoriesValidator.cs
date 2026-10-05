namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;

public class GetFilteredFiduciaryProductHistoriesValidator : AbstractValidator<GetFilteredFiduciaryProductHistoriesRequest>
{
    public GetFilteredFiduciaryProductHistoriesValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(FiduciaryProductErrors.InValidFiduciaryProductId);
    }
}
