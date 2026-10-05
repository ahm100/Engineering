namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachinery;

public class DisableFixAssetMachineryCommandValidator : AbstractValidator<DisableFixAssetMachineryCommand>
{
    public DisableFixAssetMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}