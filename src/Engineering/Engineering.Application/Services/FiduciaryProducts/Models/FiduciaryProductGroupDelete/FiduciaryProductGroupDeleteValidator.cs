
namespace Engineering.Application.Services.FiduciaryProducts.Models.FiduciaryProductGroupDelete;

public class FiduciaryProductGroupDeleteValidator : AbstractValidator<FiduciaryProductGroupDeleteRequest>
{
    public FiduciaryProductGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
