
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public class ProjectStatusChangerValidator : AbstractValidator<ProjectStatusChangerRequest>
{
    public ProjectStatusChangerValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
    }
}
