namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.DeletemplementationAssistants;

public class DeleteImplementationAssistansValidator : AbstractValidator<DeleteImplementationAssistansRequest>
{
    public DeleteImplementationAssistansValidator()
    {
        RuleFor(oo => oo.ImplementationAssistantUserId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}
