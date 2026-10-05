namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationDocuments;

public class UpdateProjectOperationDocumentsValidator : AbstractValidator<UpdateProjectOperationDocumentsRequest>
{
    public UpdateProjectOperationDocumentsValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
