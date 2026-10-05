namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationDocument;

public class CreateDailyProjectOperationDocumentCommandValidator : AbstractValidator<CreateDailyProjectOperationDocumentCommand>
{
    public CreateDailyProjectOperationDocumentCommandValidator()
    {
        RuleFor(oo => oo.Url).NotEmpty().WithError(DailyProjectOperationDocumentErrors.InValidUrl);
        RuleFor(oo => oo.DailyProjectOperation).NotEmpty().WithError(DailyProjectOperationDocumentErrors.InValidDailyProjectOperation);
    }
}
