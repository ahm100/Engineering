namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.DeleteImplementationAssistans;

public class DeleteImplementationAssistansCommandValidator : AbstractValidator<DeleteImplementationAssistansCommand>
{
    public DeleteImplementationAssistansCommandValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ProjectAssistantErrors.ProjectIsEmpty);
        RuleFor(oo => oo.ImplementationAssistansId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}