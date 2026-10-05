namespace Engineering.Application.Services.RequestContractors.Models.SetRequestContractorInquiryConfirmed;

public class SetRequestContractorInquiryConfirmedValidator : AbstractValidator<SetRequestContractorInquiryConfirmedRequest>
{
    public SetRequestContractorInquiryConfirmedValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithError(RequestContractorErrors.InValidRequestContractor)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.RequestContractorInquiryId).NotNull().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
