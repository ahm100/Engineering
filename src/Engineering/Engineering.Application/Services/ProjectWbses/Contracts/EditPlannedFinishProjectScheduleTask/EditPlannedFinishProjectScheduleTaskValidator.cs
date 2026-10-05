namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedFinishProjectScheduleTask;

public class EditPlannedFinishProjectScheduleTaskValidator : AbstractValidator<EditPlannedFinishProjectScheduleTaskRequest>
{
    public EditPlannedFinishProjectScheduleTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
