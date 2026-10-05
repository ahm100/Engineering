namespace Engineering.Application.Services.FixAssetMachineries.Commands.InactiveFixAssetMachinery;

public class InactiveFixAssetMachineryCommandValidator : AbstractValidator<InactiveFixAssetMachineryCommand>
{
    public InactiveFixAssetMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}