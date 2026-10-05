namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;

public class DeleteTransportationContractorPersonnelValidator : AbstractValidator<DeleteTransportationContractorPersonnelRequest>
{
    public DeleteTransportationContractorPersonnelValidator()
    {
        RuleForEach(oo => oo.Ids)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
