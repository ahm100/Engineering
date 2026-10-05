namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservationStatus;

public class UpdateMachineryReservationStatusCommandValidator : AbstractValidator<UpdateMachineryReservationStatusCommand>
{
    public UpdateMachineryReservationStatusCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.MachineryReservationStatus).NotEmpty().WithError(FixAssetMachineryErrors.InValidStatus)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}