namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.DeleteTechnicalAssistans;

public class DeleteTechnicalAssistansCommandValidator : AbstractValidator<DeleteTechnicalAssistansCommand>
{
    public DeleteTechnicalAssistansCommandValidator()
    {
        RuleFor(oo => oo.TechnicalAssistantId).NotNull().WithError(ProjectAssistantErrors.ImplementationAssistantUserIdIsEmpty);
    }
}