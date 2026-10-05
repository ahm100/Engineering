namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;

public class GetTransportationContractorPersonnelByIdValidator : AbstractValidator<GetTransportationContractorPersonnelByIdRequest>
{
    public GetTransportationContractorPersonnelByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
