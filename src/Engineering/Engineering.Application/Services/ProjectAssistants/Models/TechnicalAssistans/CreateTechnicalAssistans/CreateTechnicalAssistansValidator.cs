namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;

public class CreateTechnicalAssistansValidator : AbstractValidator<CreateTechnicalAssistansRequest>
{
    public CreateTechnicalAssistansValidator()
    {
        RuleFor(oo => oo.TechnicalAssistantUserId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectAssistantErrors.ProjectIsEmpty);
    }
}
