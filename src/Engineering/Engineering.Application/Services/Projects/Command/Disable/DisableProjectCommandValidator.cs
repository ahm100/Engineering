namespace Engineering.Application.Services.Projects.Commands.Disable;

public class DisableProjectCommandValidator : AbstractValidator<DisableProjectCommand>
{
    public DisableProjectCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectId);
    }
}