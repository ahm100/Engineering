namespace Engineering.Application.Services.ContractorMachineries.Commands.StateChangerContractorMachineries;

public class StateChangerContractorMachineriesCommandValidator : AbstractValidator<StateChangerContractorMachineriesCommand>
{
    public StateChangerContractorMachineriesCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
