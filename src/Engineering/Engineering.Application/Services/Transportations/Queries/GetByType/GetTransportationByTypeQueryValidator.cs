
namespace Engineering.Application.Services.Transportations.Queries.GetByType;

public class GetTransportationByTypeQueryValidator : AbstractValidator<GetTransportationByTypeQuery>
{
    public GetTransportationByTypeQueryValidator()
    {
        RuleFor(oo => oo.TransportationType).IsInEnum().WithError(TransportationErrors.TransportationTypeIsEmpty);
    }
}