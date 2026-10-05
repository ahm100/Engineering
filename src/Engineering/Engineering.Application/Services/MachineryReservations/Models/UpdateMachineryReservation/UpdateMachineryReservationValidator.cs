namespace Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservation;

public class UpdateMachineryReservationValidator : AbstractValidator<UpdateMachineryReservationRequest>
{
    public UpdateMachineryReservationValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotEmpty().WithError(MachineryReservationErrors.RequestIdIsEmpty);
        RuleFor(oo => oo.FixAssetMachineryId).NotEmpty().WithError(MachineryReservationErrors.FixIdIsEmpty);
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
