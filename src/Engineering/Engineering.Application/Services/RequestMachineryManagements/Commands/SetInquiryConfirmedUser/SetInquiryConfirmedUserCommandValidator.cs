namespace Engineering.Application.Services.RequestMachineryManagements.Commands.SetInquiryConfirmedUser;

public class SetInquiryConfirmedUserCommandValidator : AbstractValidator<SetInquiryConfirmedUserCommand>
{
    public SetInquiryConfirmedUserCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryInquiry).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
    }
}
