namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiry;

public class DeleteRequestContractorInquiryCommandValidator : AbstractValidator<DeleteRequestContractorInquiryCommand>
{
    public DeleteRequestContractorInquiryCommandValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiry).NotNull().NotEmpty().WithError(RequestContractorInquiryErrors.InValidRequestContractorInquiry);
    }
}
