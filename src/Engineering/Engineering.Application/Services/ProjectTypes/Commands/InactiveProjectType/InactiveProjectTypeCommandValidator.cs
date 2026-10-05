namespace Engineering.Application.Services.ProjectTypes.Commands.InactiveProjectType;

public class InactiveProjectTypeCommandValidator : AbstractValidator<InactiveProjectTypeCommand>
{
    public InactiveProjectTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectTypeErrors.IdIsEmpty);
    }
}