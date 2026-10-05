namespace Engineering.Application.Services.ProjectOperations.Models.SetProjectOperationPriority;

public class SetProjectOperationPriorityValidator : AbstractValidator<SetProjectOperationPriorityRequest>
{
    public SetProjectOperationPriorityValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationErrors.IdIsEmpty);
    }
}