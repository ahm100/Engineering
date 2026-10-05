namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiryDocument;

public class DeleteRequestMachineryInquiryDocumentCommandValidator : AbstractValidator<DeleteRequestMachineryInquiryDocumentCommand>
{
    public DeleteRequestMachineryInquiryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull();
    }
}
