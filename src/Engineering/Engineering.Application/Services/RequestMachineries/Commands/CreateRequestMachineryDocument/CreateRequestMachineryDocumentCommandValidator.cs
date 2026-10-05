namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachineryDocument;

public class CreateRequestMachineryDocumentCommandValidator : AbstractValidator<CreateRequestMachineryDocumentCommand>
{
    public CreateRequestMachineryDocumentCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotEmpty();
        RuleFor(oo => oo.Url).NotEmpty();
    }
}
