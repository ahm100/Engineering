namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskDurationDays;

public class EditProjectScheduleTaskDurationDaysValidator : AbstractValidator<EditProjectScheduleTaskDurationDaysRequest>
{
    public EditProjectScheduleTaskDurationDaysValidator()
    {
        RuleFor(x => x.TaskId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}