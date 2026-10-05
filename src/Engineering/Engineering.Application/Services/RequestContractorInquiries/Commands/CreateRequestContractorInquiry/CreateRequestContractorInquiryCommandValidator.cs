namespace Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiry;

public class CreateRequestContractorInquiryCommandValidator : AbstractValidator<CreateRequestContractorInquiryCommand>
{
    public CreateRequestContractorInquiryCommandValidator()
    {
        RuleFor(oo => oo.ContractorId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.UnvalidContractorId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.UnvalidContractorId);
        RuleFor(oo => oo.Amount).NotNull().WithError(RequestContractorInquiryErrors.InValidAmount);
        RuleFor(oo => oo.TotalAmount).NotNull().WithError(RequestContractorInquiryErrors.InValidTotalAmount);
        RuleFor(oo => oo.Type).IsInEnum().WithError(RequestContractorInquiryErrors.InValidType);
        RuleFor(oo => oo.CurrencyId).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidCurrencyId)
            .GreaterThan(0).WithError(RequestContractorInquiryErrors.InValidCurrencyId);
        RuleFor(oo => oo.RequestContractor).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractor);
    }
}
