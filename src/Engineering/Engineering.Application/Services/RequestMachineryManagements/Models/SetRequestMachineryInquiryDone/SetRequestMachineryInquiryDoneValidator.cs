namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryDone;

public class SetRequestMachineryInquiryDoneValidator : AbstractValidator<SetRequestMachineryInquiryDoneRequest>
{
    public SetRequestMachineryInquiryDoneValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.RequestMachineryInquiryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
    }
}
