namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDocument;

public class UpdateRequestMachineryDocumentCommandValidator : AbstractValidator<UpdateRequestMachineryDocumentCommand>
{
    public UpdateRequestMachineryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Url).NotEmpty();
    }
}
