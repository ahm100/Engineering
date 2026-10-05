namespace Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;

public class DeleteTransportationContractorValidator : AbstractValidator<DeleteTransportationContractorRequest>
{
    public DeleteTransportationContractorValidator()
    {
        RuleForEach(oo => oo.Ids)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
