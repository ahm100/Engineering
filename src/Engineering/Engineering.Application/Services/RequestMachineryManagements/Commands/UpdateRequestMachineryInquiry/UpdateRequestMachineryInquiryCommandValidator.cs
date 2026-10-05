namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiry;

public class UpdateRequestMachineryInquiryCommandValidator : AbstractValidator<UpdateRequestMachineryInquiryCommand>
{
    public UpdateRequestMachineryInquiryCommandValidator()
    {
        RuleFor(oo => oo.ThirdPartyId).NotNull().GreaterThanOrEqualTo(1).WithMessage(RequestMachineryInquiryErrors.InValidThirdPartyId);
        RuleFor(oo => oo.Count).NotNull().WithMessage(RequestMachineryInquiryErrors.InValidCount);
        RuleFor(oo => oo.InquiryRequestedTime).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestedTime);
        RuleFor(oo => oo.Unit).IsInEnum().WithMessage(RequestMachineryInquiryErrors.InValidUnit);
        RuleFor(oo => oo.UnitPrice).NotNull().WithMessage(RequestMachineryInquiryErrors.InValidUnitPrice);
        RuleFor(oo => oo.TotalPrice).NotNull().WithMessage(RequestMachineryInquiryErrors.InValidTotalPrice);
        RuleFor(oo => oo.RequestMachineryInquiryId).NotNull().GreaterThanOrEqualTo(1).WithMessage(RequestMachineryInquiryErrors.InValidRequestMachineryInquiry);
    }
}
