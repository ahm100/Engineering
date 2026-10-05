
namespace Engineering.Application.Services.Trips.Queries.GetByName;

public class GetTripByNameQueryValidator : AbstractValidator<GetTripByNameQuery>
{
    public GetTripByNameQueryValidator()
    {
        RuleFor(oo => oo.TripName).NotEmpty().WithError(TripErrors.NameIsEmpty);
    }
}
