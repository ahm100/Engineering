namespace Engineering.Application.Services.RequestMachineryManagements.Commands.ConfirmRequestMachineryInquiry;

public class ConfirmRequestMachineryInquiryCommandValidator : AbstractValidator<ConfirmRequestMachineryInquiryCommand>
{
    public ConfirmRequestMachineryInquiryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryInquiryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
    }
}
