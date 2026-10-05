namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;

public class GetsProjectOperationDetailDocumentValidator : AbstractValidator<GetsProjectOperationDetailDocumentRequest>
{
    public GetsProjectOperationDetailDocumentValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailErrors.IdIsEmpty);
    }
}
