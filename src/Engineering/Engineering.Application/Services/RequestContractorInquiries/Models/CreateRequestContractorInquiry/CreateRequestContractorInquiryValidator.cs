namespace Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;

public class CreateRequestContractorInquiryValidator : AbstractValidator<CreateRequestContractorInquiryRequest>
{
    public CreateRequestContractorInquiryValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractor);

        RuleForEach(c => c.Inquiries)
            .NotEmpty()
            .WithError(RequestContractorInquiryErrors.InvalidList)
            .SetValidator(new CreateRequestContractorInquiryRequestModelValidator());
    }
}
