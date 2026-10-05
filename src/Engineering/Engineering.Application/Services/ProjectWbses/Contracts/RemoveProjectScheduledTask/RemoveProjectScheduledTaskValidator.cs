namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduledTask;

public class RemoveProjectScheduledTaskValidator : AbstractValidator<RemoveProjectScheduledTaskRequest>
{
    public RemoveProjectScheduledTaskValidator()
    {
        RuleFor(x => x.TaskId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
