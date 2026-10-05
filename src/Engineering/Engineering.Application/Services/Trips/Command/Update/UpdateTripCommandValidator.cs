
namespace Engineering.Application.Services.Trips.Commands.Update;

public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommand>
{
    public UpdateTripCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TripErrors.IdIsEmpty);
        RuleFor(oo => oo.TripName).NotEmpty().WithError(TripErrors.NameIsEmpty);
        RuleFor(oo => oo.TripCode).NotEmpty().WithError(TripErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TripErrors.IsActiveIsEmpty);
    }
}
