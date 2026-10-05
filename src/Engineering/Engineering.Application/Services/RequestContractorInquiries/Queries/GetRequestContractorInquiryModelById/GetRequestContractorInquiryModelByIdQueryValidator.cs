namespace Engineering.Application.Services.RequestContractorInquiries.Queries.GetRequestContractorInquiryModelById;

public class GetRequestContractorInquiryModelByIdQueryValidator : AbstractValidator<GetRequestContractorInquiryModelByIdQuery>
{
    public GetRequestContractorInquiryModelByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiryId).NotNull().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry);
    }
}
