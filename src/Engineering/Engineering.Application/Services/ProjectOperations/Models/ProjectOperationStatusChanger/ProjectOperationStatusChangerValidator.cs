
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public class ProjectOperationStatusChangerValidator : AbstractValidator<ProjectOperationStatusChangerRequest>
{
    public ProjectOperationStatusChangerValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
