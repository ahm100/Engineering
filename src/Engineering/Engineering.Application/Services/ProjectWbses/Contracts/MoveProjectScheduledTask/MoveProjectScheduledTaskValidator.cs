namespace Engineering.Application.Services.ProjectWbses.Contracts.MoveProjectScheduledTask;

public class MoveProjectScheduledTaskValidator : AbstractValidator<MoveProjectScheduledTaskRequest>
{
    public MoveProjectScheduledTaskValidator()
    {
        RuleFor(e => e.TaskId)
            .IsPositive(GlobalCmts.Id);
    }
}
