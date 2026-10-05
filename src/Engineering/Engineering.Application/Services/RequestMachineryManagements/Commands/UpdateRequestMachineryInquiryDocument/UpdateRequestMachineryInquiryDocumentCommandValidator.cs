namespace Engineering.Application.Services.RequestMachineryManagements.Commands.UpdateRequestMachineryInquiryDocument;

public class UpdateRequestMachineryInquiryDocumentCommandValidator : AbstractValidator<UpdateRequestMachineryInquiryDocumentCommand>
{
    public UpdateRequestMachineryInquiryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Url).NotEmpty();
    }
}
