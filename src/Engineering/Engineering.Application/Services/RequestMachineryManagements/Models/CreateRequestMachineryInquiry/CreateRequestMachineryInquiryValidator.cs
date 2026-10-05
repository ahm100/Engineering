namespace Engineering.Application.Services.RequestMachineryManagements.Models.CreateRequestMachineryInquiry;

public class CreateRequestMachineryInquiryValidator : AbstractValidator<CreateRequestMachineryInquiryRequest>
{
    public CreateRequestMachineryInquiryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery);
        RuleForEach(oo => oo.Inquiries).NotEmpty().SetValidator(new CreateRequestMachineryInquiryRequestModelValidator());
    }
}
