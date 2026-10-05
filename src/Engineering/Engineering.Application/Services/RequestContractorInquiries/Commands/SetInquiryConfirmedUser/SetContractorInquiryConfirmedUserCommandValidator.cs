namespace Engineering.Application.Services.RequestContractorInquiries.Commands.SetContractorInquiryConfirmedUser;

public class SetContractorInquiryConfirmedUserCommandValidator : AbstractValidator<SetContractorInquiryConfirmedUserCommand>
{
    public SetContractorInquiryConfirmedUserCommandValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiry).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry);
    }
}
