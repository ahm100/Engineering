namespace Engineering.Application.Services.Projects.Commands.InactiveProject;

public class InactiveProjectCommandValidator : AbstractValidator<InactiveProjectCommand>
{
    public InactiveProjectCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}