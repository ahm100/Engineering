namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationDocument;

public class DeleteDailyProjectOperationDocumentCommandValidator : AbstractValidator<DeleteDailyProjectOperationDocumentCommand>
{
    public DeleteDailyProjectOperationDocumentCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationDocumentId).NotNull().WithError(DailyProjectOperationDocumentErrors.InValidDailyProjectOperationDocumentId);
    }
}
