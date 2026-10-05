namespace Engineering.Application.Services.Projects.Commands.StateChangerProjects;

public class StateChangerProjectsCommandValidator : AbstractValidator<StateChangerProjectsCommand>
{
    public StateChangerProjectsCommandValidator()
    {
        RuleFor(oo => oo.Items).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
