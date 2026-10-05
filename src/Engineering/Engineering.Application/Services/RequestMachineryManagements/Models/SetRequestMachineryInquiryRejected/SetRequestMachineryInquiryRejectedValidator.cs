namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryRejected;

public class SetRequestMachineryInquiryRejectedValidator : AbstractValidator<SetRequestMachineryInquiryRejectedRequest>
{
    public SetRequestMachineryInquiryRejectedValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithMessage(RequestMachineryErrors.InValidRequestMachinery);
    }
}
