namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryConfirmed;

public class SetRequestMachineryConfirmedValidator : AbstractValidator<SetRequestMachineryConfirmedRequest>
{
    public SetRequestMachineryConfirmedValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.OperatorAppoinmentId).NotNull().WithError(RequestMachineryErrors.InValidOperatorAppointmentId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
