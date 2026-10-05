namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.CreateImplementationAssistans;

public class CreateImplementationAssistansCommandValidator : AbstractValidator<CreateImplementationAssistansCommand>
{
    public CreateImplementationAssistansCommandValidator()
    {
        RuleFor(oo => oo.Project).NotEmpty().WithError(ProjectAssistantErrors.ProjectIsEmpty);
        RuleFor(oo => oo.ImplementationAssistantUserId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}