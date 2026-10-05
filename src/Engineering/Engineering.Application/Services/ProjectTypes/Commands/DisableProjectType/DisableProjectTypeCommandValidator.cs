namespace Engineering.Application.Services.ProjectTypes.Commands.DisableProjectType;

public class DisableProjectTypeCommandValidator : AbstractValidator<DisableProjectTypeCommand>
{
    public DisableProjectTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectTypeErrors.IdIsEmpty);
    }
}