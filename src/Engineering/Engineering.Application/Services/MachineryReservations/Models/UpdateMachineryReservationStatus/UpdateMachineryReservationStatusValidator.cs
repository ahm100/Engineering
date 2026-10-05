namespace Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservationStatus;

public class UpdateMachineryReservationStatusValidator : AbstractValidator<UpdateMachineryReservationStatusRequest>
{
    public UpdateMachineryReservationStatusValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(FixAssetMachineryErrors.IdIsEmpty)
             .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.MachineryReservationStatus).NotEmpty().WithError(FixAssetMachineryErrors.InValidStatus)
            .IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
