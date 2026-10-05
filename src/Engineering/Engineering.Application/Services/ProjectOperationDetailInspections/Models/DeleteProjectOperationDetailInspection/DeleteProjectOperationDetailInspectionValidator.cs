namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.DeleteProjectOperationDetailInspection;

public class DeleteProjectOperationDetailInspectionValidator : AbstractValidator<DeleteProjectOperationDetailInspectionRequest>
{
    public DeleteProjectOperationDetailInspectionValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailInspectionErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
