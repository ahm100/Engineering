namespace Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachinery;

public class DisableFixAssetMachineryValidator : AbstractValidator<DisableFixAssetMachineryRequest>
{
    public DisableFixAssetMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
