namespace Engineering.Application.Services.FixAssetMachineries.Commands.DisableFixAssetMachineryNotWork;

public class DisableFixAssetMachineryNotWorkCommandValidator : AbstractValidator<DisableFixAssetMachineryNotWorkCommand>
{
    public DisableFixAssetMachineryNotWorkCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}