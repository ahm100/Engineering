namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryAppointment;

public class SetRequestMachineryInquiryAppointmentValidator : AbstractValidator<SetRequestMachineryInquiryAppointmentRequest>
{
    public SetRequestMachineryInquiryAppointmentValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.RequestMachineryInquiryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
    }
}
