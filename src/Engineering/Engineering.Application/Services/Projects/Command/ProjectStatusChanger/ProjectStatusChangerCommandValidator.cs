
namespace Engineering.Application.Services.Projects.Commands.ProjectStatusChanger;

public class ProjectStatusChangerCommandValidator : AbstractValidator<ProjectStatusChangerCommand>
{
    public ProjectStatusChangerCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
        RuleFor(oo => oo.Status)
            .IsEnum(GlobalCmts.Status);
    }
}
