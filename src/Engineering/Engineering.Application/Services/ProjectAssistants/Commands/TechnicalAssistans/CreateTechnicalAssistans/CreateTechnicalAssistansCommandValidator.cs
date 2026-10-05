namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.CreateTechnicalAssistans;

public class CreateExpertCommandValidator : AbstractValidator<CreateTechnicalAssistansCommand>
{
    public CreateExpertCommandValidator()
    {
        RuleFor(oo => oo.Project).NotEmpty().WithError(ProjectAssistantErrors.ProjectIsEmpty);
        RuleFor(oo => oo.TechnicalAssistantUserId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}