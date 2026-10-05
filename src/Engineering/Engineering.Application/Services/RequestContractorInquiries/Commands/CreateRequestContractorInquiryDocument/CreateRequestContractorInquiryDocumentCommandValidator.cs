namespace Engineering.Application.Services.RequestContractorInquiries.Commands.CreateRequestContractorInquiryDocument;

public class CreateRequestContractorInquiryDocumentCommandValidator : AbstractValidator<CreateRequestContractorInquiryDocumentCommand>
{
    public CreateRequestContractorInquiryDocumentCommandValidator()
    {
        RuleFor(oo => oo.RequestContractorInquiry).NotEmpty();
        RuleFor(oo => oo.Url).NotEmpty();
    }
}
