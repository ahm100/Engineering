namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationDocuments;

public class UpdateProjectOperationDocumentsCommandValidator : AbstractValidator<UpdateProjectOperationDocumentsCommand>
{
    public UpdateProjectOperationDocumentsCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
