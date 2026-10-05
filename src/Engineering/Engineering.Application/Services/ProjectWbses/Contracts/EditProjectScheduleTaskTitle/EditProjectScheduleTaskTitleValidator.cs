namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskTitle;

public class EditProjectScheduleTaskTitleValidator : AbstractValidator<EditProjectScheduleTaskTitleRequest>
{
    public EditProjectScheduleTaskTitleValidator()
    {
        RuleFor(x => x.TaskId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}