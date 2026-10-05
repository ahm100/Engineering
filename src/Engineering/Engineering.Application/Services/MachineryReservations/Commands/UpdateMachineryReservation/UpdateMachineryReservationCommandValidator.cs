namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservation;

public class UpdateMachineryReservationCommandValidator : AbstractValidator<UpdateMachineryReservationCommand>
{
    public UpdateMachineryReservationCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryReservationErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.RequestMachinery).NotEmpty().WithError(MachineryReservationErrors.InValidMachineryReservation);
        RuleFor(oo => oo.FixAssetMachinery).NotEmpty().WithError(MachineryReservationErrors.InValidMachineryReservation);
        RuleFor(oo => oo.MachineryReservationUnit).NotEmpty().WithError(MachineryReservationErrors.InValidType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum);
        RuleFor(oo => oo.StartDate)
             .NotEmpty().WithError(GlobalErrors.StartDateIsNull);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(GlobalErrors.EndDateIsNull);

        RuleFor(oo => oo.StartDate.Date)
            .LessThan(oo => oo.EndDate.Date).WithError(GlobalErrors.StartDateCanNotBigerToEndDate);

        RuleFor(oo => oo.EndDate.Date)
            .GreaterThan(oo => oo.StartDate.Date).WithError(GlobalErrors.EndDateCanNotSmallerToStartDate);
    }
}