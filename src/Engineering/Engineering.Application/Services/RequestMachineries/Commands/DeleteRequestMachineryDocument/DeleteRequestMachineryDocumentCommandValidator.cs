namespace Engineering.Application.Services.RequestMachineries.Commands.DeleteRequestMachineryDocument;

public class DeleteRequestMachineryDocumentCommandValidator : AbstractValidator<DeleteRequestMachineryDocumentCommand>
{
    public DeleteRequestMachineryDocumentCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull();
    }
}
