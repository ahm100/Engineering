namespace Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;

public class CreateRequestMachineryInquiryRequestModelValidator : AbstractValidator<CreateRequestMachineryInquiryRequestModel>
{
    public CreateRequestMachineryInquiryRequestModelValidator()
    {
        RuleFor(oo => oo.Count).NotNull().WithError(RequestMachineryInquiryErrors.InValidCount);
        RuleFor(oo => oo.InquiryRequestedTime).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestedTime);
        RuleFor(oo => oo.UnitPrice).NotEmpty().WithError(RequestMachineryInquiryErrors.InValidUnitPrice);
        RuleFor(oo => oo.UnitPrice).LessThan(9999999999999999).WithError(RequestMachineryInquiryErrors.UnitPriceCanNotGreater);
        //RuleFor(oo => oo.TotalPrice).NotEmpty().WithError(RequestMachineryInquiryErrors.InValidTotalPrice);
        //RuleFor(oo => oo.TotalPrice).LessThan(9999999999999999).WithError(RequestMachineryInquiryErrors.TotalPriceCanNotGreater);
        RuleFor(oo => oo.Unit).IsInEnum().WithError(RequestMachineryInquiryErrors.InValidUnit);
        RuleFor(oo => oo.CurrencyId).NotNull().WithError(RequestMachineryInquiryErrors.InValidCurrency);
    }
}