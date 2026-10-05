namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailDocument;

public class GetsProjectOperationDetailDocumentQueryValidator : AbstractValidator<GetsProjectOperationDetailDocumentQuery>
{
    public GetsProjectOperationDetailDocumentQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
