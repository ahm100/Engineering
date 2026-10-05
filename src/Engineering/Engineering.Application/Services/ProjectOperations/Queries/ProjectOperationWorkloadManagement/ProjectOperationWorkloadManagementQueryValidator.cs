namespace Engineering.Application.Services.ProjectOperations.Queries.ProjectOperationWorkloadManagement;

public class ProjectOperationWorkloadManagementQueryValidator : AbstractValidator<ProjectOperationWorkloadManagementQuery>
{
    public ProjectOperationWorkloadManagementQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
