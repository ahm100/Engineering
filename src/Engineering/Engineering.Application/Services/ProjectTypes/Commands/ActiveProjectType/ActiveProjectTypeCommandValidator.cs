namespace Engineering.Application.Services.ProjectTypes.Commands.ActiveProjectType;

public class ActiveProjectTypeCommandValidator : AbstractValidator<ActiveProjectTypeCommand>
{
    public ActiveProjectTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectTypeErrors.IdIsEmpty);
    }
}