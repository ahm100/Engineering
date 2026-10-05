namespace Engineering.Application.Services.RequestContractorInquiries.Commands.UpdateRequestContractorInquiry;

public class UpdateRequestContractorInquiryCommandValidator : AbstractValidator<UpdateRequestContractorInquiryCommand>
{
    public UpdateRequestContractorInquiryCommandValidator()
    {
        RuleFor(oo => oo.ContractorId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.UnvalidContractorId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.UnvalidContractorId);
        RuleFor(oo => oo.Amount).NotNull().WithError(RequestContractorInquiryErrors.InValidAmount);
        RuleFor(oo => oo.TotalAmount).NotNull().WithError(RequestContractorInquiryErrors.InValidTotalAmount);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestContractorInquiryErrors.InValidType);
        RuleFor(oo => oo.CurrencyId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidCurrencyId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.InValidCurrencyId);
        RuleFor(oo => oo.RequestContractorInquiry).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry);
    }
}
