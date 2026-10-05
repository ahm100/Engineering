
namespace Engineering.Application.Services.Transportations.Queries.GetByCode;

public class GetTransportationByCodeQueryValidator : AbstractValidator<GetTransportationByCodeQuery>
{
    public GetTransportationByCodeQueryValidator()
    {
        RuleFor(oo => oo.TransportationCode).NotEmpty().WithError(TransportationErrors.CodeIsEmpty);
    }
}
