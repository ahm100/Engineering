
namespace Engineering.Application.Services.Trips.Models.Update;

public class UpdateTripValidator : AbstractValidator<UpdateTripRequest>
{
    public UpdateTripValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(TripErrors.IdIsEmpty);
        RuleFor(oo => oo.TripName).NotEmpty().WithError(TripErrors.NameIsEmpty);
        RuleFor(oo => oo.TripCode).NotEmpty().WithError(TripErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TripErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.TripName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.TripCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
