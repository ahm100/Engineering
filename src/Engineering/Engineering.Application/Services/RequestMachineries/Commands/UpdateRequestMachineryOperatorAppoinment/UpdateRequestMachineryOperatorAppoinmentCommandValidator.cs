namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryOperatorAppoinment;

public class UpdateRequestMachineryOperatorAppoinmentCommandValidator : AbstractValidator<UpdateRequestMachineryOperatorAppoinmentCommand>
{
    public UpdateRequestMachineryOperatorAppoinmentCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.OperatorAppoinmentId).NotNull().WithError(RequestMachineryErrors.InValidOperatorAppointmentId);
        RuleFor(oo => oo.OperatorAppoinmentUserId).NotNull().WithError(RequestMachineryErrors.InValidOperatorAppointmentUserId);
    }
}
