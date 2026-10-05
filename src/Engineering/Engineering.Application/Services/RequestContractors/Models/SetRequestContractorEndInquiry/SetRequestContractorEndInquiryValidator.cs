namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorEndInquiry;

public class SetRequestContractorEndInquiryValidator : AbstractValidator<SetRequestContractorEndInquiryRequest>
{
    public SetRequestContractorEndInquiryValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithMessage(RequestContractorErrors.InValidRequestContractor);
    }
}
