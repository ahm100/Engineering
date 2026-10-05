
namespace Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationStatusChanger;

public class ProjectOperationStatusChangerCommandValidator : AbstractValidator<ProjectOperationStatusChangerCommand>
{
    public ProjectOperationStatusChangerCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(ProjectErrors.StatusIsEmpty);
    }
}
