
namespace Engineering.Application.Services.Trips.Queries.GetByCode;

public class GetTripByCodeQueryValidator : AbstractValidator<GetTripByCodeQuery>
{
    public GetTripByCodeQueryValidator()
    {
        RuleFor(oo => oo.TripCode).NotEmpty().WithError(TripErrors.CodeIsEmpty);
    }
}
