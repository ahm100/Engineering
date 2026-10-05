namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;

public class GetRequestContractorInquiryByIdValidator : AbstractValidator<GetRequestContractorInquiryByIdRequest>
{
    public GetRequestContractorInquiryByIdValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiryId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
