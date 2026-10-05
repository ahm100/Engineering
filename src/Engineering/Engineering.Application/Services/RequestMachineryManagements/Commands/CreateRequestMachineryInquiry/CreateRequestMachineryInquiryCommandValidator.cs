namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;

public class CreateRequestMachineryInquiryCommandValidator : AbstractValidator<CreateRequestMachineryInquiryCommand>
{
    public CreateRequestMachineryInquiryCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().GreaterThanOrEqualTo(1).WithError(RequestMachineryInquiryErrors.InValidThirdPartyId);
        RuleFor(oo => oo.Count).NotNull().WithError(RequestMachineryInquiryErrors.InValidCount);
        RuleFor(oo => oo.InquiryRequestedTime).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestedTime);
        RuleFor(oo => oo.Unit).IsInEnum().WithError(RequestMachineryInquiryErrors.InValidUnit);
        RuleFor(oo => oo.UnitPrice).NotNull().WithError(RequestMachineryInquiryErrors.InValidUnitPrice);
        RuleFor(oo => oo.TotalPrice).NotNull().WithError(RequestMachineryInquiryErrors.InValidTotalPrice);
        RuleFor(oo => oo.RequestMachineryInquiryOperator).NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachineryInquiryOperator);
    }
}
