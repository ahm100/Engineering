namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryById;

public class GetRequestContractorInquiryByIdQueryValidator : AbstractValidator<GetRequestContractorInquiryByIdQuery>
{
    public GetRequestContractorInquiryByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiryId).NotNull().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry);
    }
}
