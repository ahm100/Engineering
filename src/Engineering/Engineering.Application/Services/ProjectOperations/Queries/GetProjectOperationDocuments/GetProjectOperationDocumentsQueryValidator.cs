namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationDocuments;

public class GetProjectOperationDocumentsQueryValidator : AbstractValidator<GetProjectOperationDocumentsQuery>
{
    public GetProjectOperationDocumentsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
