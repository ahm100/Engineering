
namespace Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;

public class CreateMachineryReservationValidator : AbstractValidator<CreateMachineryReservationRequest>
{
    public CreateMachineryReservationValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotEmpty().WithError(MachineryReservationErrors.RequestIdIsEmpty);
        RuleFor(oo => oo.FixAssetMachineryId).NotEmpty().WithError(MachineryReservationErrors.FixIdIsEmpty);
        RuleFor(oo => oo.MachineryReservationUnit).NotEmpty().WithError(MachineryReservationErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
        RuleFor(oo => oo.StartDate)
             .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);
    }
}
