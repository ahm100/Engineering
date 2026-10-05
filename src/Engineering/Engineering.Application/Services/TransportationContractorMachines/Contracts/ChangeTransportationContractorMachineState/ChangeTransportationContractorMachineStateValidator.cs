namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;

public class ChangeTransportationContractorMachineStateValidator : AbstractValidator<ChangeTransportationContractorMachineStateRequest>
{
    public ChangeTransportationContractorMachineStateValidator()
    {
        RuleForEach(c => c.Ids)
            .IsPositive(TransportationContractorMachineCmts.TransportationContractorMachineId);
    }
}
