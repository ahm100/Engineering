namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiryDocument;

public class DeleteRequestContractorInquiryDocumentCommandValidator : AbstractValidator<DeleteRequestContractorInquiryDocumentCommand>
{
    public DeleteRequestContractorInquiryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull();
    }
}
