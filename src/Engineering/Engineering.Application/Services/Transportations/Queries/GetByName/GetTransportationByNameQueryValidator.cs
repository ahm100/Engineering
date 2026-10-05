
namespace Engineering.Application.Services.Transportations.Queries.GetByName;

public class GetTransportationByNameQueryValidator : AbstractValidator<GetTransportationByNameQuery>
{
    public GetTransportationByNameQueryValidator()
    {
        RuleFor(oo => oo.TransportationName).NotEmpty().WithError(TransportationErrors.NameIsEmpty);
    }
}