namespace Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryRate;

public class DisableFixAssetMachineryRateValidator : AbstractValidator<DisableFixAssetMachineryRateRequest>
{
    public DisableFixAssetMachineryRateValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
