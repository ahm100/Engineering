namespace Engineering.Application.Services.ProjectWbses.Contracts.EditActualStartProjectScheduleTask;

public class EditActualStartProjectScheduleTaskValidator : AbstractValidator<EditActualStartProjectScheduleTaskRequest>
{
    public EditActualStartProjectScheduleTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
