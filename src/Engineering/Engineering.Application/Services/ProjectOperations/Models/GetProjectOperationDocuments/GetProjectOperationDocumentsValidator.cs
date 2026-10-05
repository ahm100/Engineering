namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationDocuments;

public class GetProjectOperationDocumentsValidator : AbstractValidator<GetProjectOperationDocumentsRequest>
{
    public GetProjectOperationDocumentsValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
