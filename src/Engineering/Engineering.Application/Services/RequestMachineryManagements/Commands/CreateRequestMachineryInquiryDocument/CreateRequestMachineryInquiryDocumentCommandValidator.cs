namespace Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryDocument;

public class CreateRequestMachineryInquiryDocumentCommandValidator : AbstractValidator<CreateRequestMachineryInquiryDocumentCommand>
{
    public CreateRequestMachineryInquiryDocumentCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryInquiry).NotEmpty();
        RuleFor(oo => oo.Url).NotEmpty();
    }
}
