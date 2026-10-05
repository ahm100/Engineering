
namespace Engineering.Application.Services.Trips.Models.GetByName;

public class GetTripByNameValidator : AbstractValidator<GetTripByNameRequest>
{
    public GetTripByNameValidator()
    {
        RuleFor(oo => oo.TripName).NotEmpty().WithError(TripErrors.NameIsEmpty);
    }
}
