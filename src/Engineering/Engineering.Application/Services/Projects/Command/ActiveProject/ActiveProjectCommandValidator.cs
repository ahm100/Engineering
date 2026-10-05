namespace Engineering.Application.Services.Projects.Commands.ActiveProject;

public class ActiveProjectCommandValidator : AbstractValidator<ActiveProjectCommand>
{
    public ActiveProjectCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}