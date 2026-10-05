namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryRate;

public class DisableFixAssetMachineryRateCommandValidator : AbstractValidator<DisableFixAssetMachineryRateCommand>
{
    public DisableFixAssetMachineryRateCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}