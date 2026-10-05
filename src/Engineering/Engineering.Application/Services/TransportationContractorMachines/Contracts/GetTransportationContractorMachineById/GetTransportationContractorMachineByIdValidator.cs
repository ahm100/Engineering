namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;

public class GetTransportationContractorMachineByIdValidator : AbstractValidator<GetTransportationContractorMachineByIdRequest>
{
    public GetTransportationContractorMachineByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
