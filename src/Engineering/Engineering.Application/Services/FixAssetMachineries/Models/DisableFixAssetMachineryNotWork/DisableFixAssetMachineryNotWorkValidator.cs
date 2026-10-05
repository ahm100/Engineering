namespace Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryNotWork;

public class DisableFixAssetMachineryNotWorkValidator : AbstractValidator<DisableFixAssetMachineryNotWorkRequest>
{
    public DisableFixAssetMachineryNotWorkValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
