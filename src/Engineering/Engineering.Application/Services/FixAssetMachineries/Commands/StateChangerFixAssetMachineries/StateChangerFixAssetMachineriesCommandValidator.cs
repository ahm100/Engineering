namespace Engineering.Application.Services.FixAssetMachineries.Commands.StateChangerFixAssetMachineries;

public class StateChangerFixAssetMachineriesCommandValidator : AbstractValidator<StateChangerFixAssetMachineriesCommand>
{
    public StateChangerFixAssetMachineriesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
