namespace Engineering.Application.Services.ProjectOperations.Commands.SetProjectOperationPriority;

public class SetProjectOperationPriorityCommandValidator : AbstractValidator<SetProjectOperationPriorityCommand>
{
    public SetProjectOperationPriorityCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
