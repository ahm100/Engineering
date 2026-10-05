namespace Engineering.Application.Services.MachineryReservations.Commands.CreateMachineryReservation;

public class CreateMachineryReservationCommandValidator : AbstractValidator<CreateMachineryReservationCommand>
{
    public CreateMachineryReservationCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotEmpty().WithError(MachineryReservationErrors.InValidMachineryReservation);
        RuleFor(oo => oo.FixAssetMachinery).NotEmpty().WithError(MachineryReservationErrors.InValidMachineryReservation);
        RuleFor(oo => oo.MachineryReservationUnit).NotEmpty().WithError(MachineryReservationErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);

        RuleFor(oo => oo.StartDate)
             .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThanOrEqualTo(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThanOrEqualTo(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);
    }
}