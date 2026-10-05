namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedStartProjectScheduleTask;

public class EditPlannedStartProjectScheduleTaskValidator : AbstractValidator<EditPlannedStartProjectScheduleTaskRequest>
{
    public EditPlannedStartProjectScheduleTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
