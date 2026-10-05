namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.CreateImplementationAssistants;

public class CreateImplementationAssistansValidator : AbstractValidator<CreateImplementationAssistansRequest>
{
    public CreateImplementationAssistansValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectAssistantErrors.ProjectIsEmpty);
        RuleFor(oo => oo.ImplementationAssistantUserId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}
