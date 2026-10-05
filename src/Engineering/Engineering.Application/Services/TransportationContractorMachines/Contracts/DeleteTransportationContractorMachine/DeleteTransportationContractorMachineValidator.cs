namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;

public class DeleteTransportationContractorMachineValidator : AbstractValidator<DeleteTransportationContractorMachineRequest>
{
    public DeleteTransportationContractorMachineValidator()
    {
        RuleForEach(oo => oo.Ids)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
