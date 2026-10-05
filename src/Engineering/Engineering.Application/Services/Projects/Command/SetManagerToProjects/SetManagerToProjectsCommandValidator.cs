namespace Engineering.Application.Services.Projects.Commands.SetManagerToProjects;

public class SetManagerToProjectsCommandValidator : AbstractValidator<SetManagerToProjectsCommand>
{
    public SetManagerToProjectsCommandValidator()
    {
        RuleFor(oo => oo.Projects).NotEmpty().WithError(ProjectErrors.IdsIsEmpty);
        RuleFor(oo => oo.ProjectManager).NotNull().WithError(ProjectErrors.EmployerIdIsEmpty)
                                      .GreaterThanOrEqualTo(1).WithError(ProjectErrors.ProjectManagerIdGreaterThanZero);
    }
}