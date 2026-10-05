namespace Engineering.Application.Services.MachineryReservations.Models.DisableMachineryReservation;

public class DisableMachineryReservationValidator : AbstractValidator<DisableMachineryReservationRequest>
{
    public DisableMachineryReservationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryReservationErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
