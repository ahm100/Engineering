
namespace Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryNotWorkGroupDelete;

public class FixAssetMachineryNotWorkGroupDeleteValidator : AbstractValidator<FixAssetMachineryNotWorkGroupDeleteRequest>
{
    public FixAssetMachineryNotWorkGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
