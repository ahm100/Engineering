
namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirplaneById;

public class GetAirplaneByIdQueryValidator : AbstractValidator<GetAirplaneByIdQuery>
{
    public GetAirplaneByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
