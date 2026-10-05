namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.DeleteTechnicalAssistans;

public class DeleteTechnicalAssistansValidator : AbstractValidator<DeleteTechnicalAssistansRequest>
{
    public DeleteTechnicalAssistansValidator()
    {
        RuleFor(oo => oo.TechnicalAssistansId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}
