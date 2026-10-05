namespace Engineering.Application.Services.MachineryReservations.Commands.DisableMachineryReservation;

public class DisableMachineryReservationCommandValidator : AbstractValidator<DisableMachineryReservationCommand>
{
    public DisableMachineryReservationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryReservationErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}