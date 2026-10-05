namespace Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;

public class GetTransportationContractorByIdValidator : AbstractValidator<GetTransportationContractorByIdRequest>
{
    public GetTransportationContractorByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(TransportationContractorErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(TransportationContractorErrors.IdIsEmpty);
    }
}
