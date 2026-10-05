namespace Engineering.Application.Services.RequestContractorInquiries.Models.UpdateRequestContractorInquiry;

public class UpdateRequestContractorInquiryValidator : AbstractValidator<UpdateRequestContractorInquiryRequest>
{
    public UpdateRequestContractorInquiryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InvalidId)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ContractorId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.UnvalidContractorId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.UnvalidContractorId);
        RuleFor(oo => oo.Amount).NotNull().WithError(RequestContractorInquiryErrors.InValidAmount);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestContractorInquiryErrors.InValidType);
        RuleFor(oo => oo.CurrencyId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidCurrencyId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.InValidCurrencyId);
    }
}
