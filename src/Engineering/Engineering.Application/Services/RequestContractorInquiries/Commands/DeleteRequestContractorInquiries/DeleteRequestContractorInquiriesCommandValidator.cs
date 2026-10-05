namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiries;

public class DeleteRequestContractorInquiriesCommandValidator : AbstractValidator<DeleteRequestContractorInquiriesCommand>
{
    public DeleteRequestContractorInquiriesCommandValidator()
    {
        RuleFor(oo => oo.RequestContractor).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractor);
    }
}
