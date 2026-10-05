namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOperatorAppointment;

public class SetRequestMachineryOperatorAppointmentValidator : AbstractValidator<SetRequestMachineryOperatorAppointmentRequest>
{
    public SetRequestMachineryOperatorAppointmentValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
