namespace Engineering.Application.Services.ProjectOperationDetailInspections.Commands.DeleteProjectOperationDetailInspection;

public class DeleteProjectOperationDetailInspectionCommandValidator : AbstractValidator<DeleteProjectOperationDetailInspectionCommand>
{
    public DeleteProjectOperationDetailInspectionCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailInspectionId).NotNull().WithError(ProjectOperationDetailInspectionErrors.IdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
