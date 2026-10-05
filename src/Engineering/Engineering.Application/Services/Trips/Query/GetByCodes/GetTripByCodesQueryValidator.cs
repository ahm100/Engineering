
namespace Engineering.Application.Services.Trips.Queries.GetByCodes;

public class GetTripByCodesQueryValidator : AbstractValidator<GetTripByCodesQuery>
{
    public GetTripByCodesQueryValidator()
    {
        RuleFor(oo => oo.TripCodes).NotEmpty().WithError(TripErrors.CodesIsEmpty);
    }
}
