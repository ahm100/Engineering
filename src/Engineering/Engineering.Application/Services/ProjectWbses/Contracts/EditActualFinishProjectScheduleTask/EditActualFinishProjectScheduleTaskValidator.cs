namespace Engineering.Application.Services.ProjectWbses.Contracts.EditActualFinishProjectScheduleTask;

public class EditActualFinishProjectScheduleTaskValidator : AbstractValidator<EditActualFinishProjectScheduleTaskRequest>
{
    public EditActualFinishProjectScheduleTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
