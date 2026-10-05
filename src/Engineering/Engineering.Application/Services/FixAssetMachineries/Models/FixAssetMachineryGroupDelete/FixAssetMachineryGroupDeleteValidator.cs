
namespace Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryGroupDelete;

public class FixAssetMachineryGroupDeleteValidator : AbstractValidator<FixAssetMachineryGroupDeleteRequest>
{
    public FixAssetMachineryGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
