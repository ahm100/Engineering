namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryRejected;

public class SetRequestContractorInquiryRejectedValidator : AbstractValidator<SetRequestContractorInquiryRejectedRequest>
{
    public SetRequestContractorInquiryRejectedValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithMessage(RequestContractorErrors.InValidRequestContractor);
    }
}
