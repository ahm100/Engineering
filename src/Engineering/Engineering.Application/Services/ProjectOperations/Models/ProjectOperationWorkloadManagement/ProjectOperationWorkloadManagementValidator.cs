namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationWorkloadManagement;

public class ProjectOperationWorkloadManagementValidator : AbstractValidator<ProjectOperationWorkloadManagementRequest>
{
    public ProjectOperationWorkloadManagementValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
