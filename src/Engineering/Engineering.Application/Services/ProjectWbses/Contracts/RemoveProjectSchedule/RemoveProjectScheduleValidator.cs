namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectSchedule;

public class RemoveProjectScheduleValidator : AbstractValidator<RemoveProjectScheduleRequest>
{
    public RemoveProjectScheduleValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id);
    }
}
