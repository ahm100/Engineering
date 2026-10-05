namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduledTask;

public class RemoveProjectScheduledTaskCommandValidator : AbstractValidator<RemoveProjectScheduledTaskCommand>
{
    public RemoveProjectScheduledTaskCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
