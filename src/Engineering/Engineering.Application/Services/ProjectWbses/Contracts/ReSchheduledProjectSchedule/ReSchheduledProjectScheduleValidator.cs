namespace Engineering.Application.Services.ProjectWbses.Contracts.ReSchheduledProjectSchedule;

public class ReSchheduledProjectScheduleValidator : AbstractValidator<ReSchheduledProjectScheduleRequest>
{
    public ReSchheduledProjectScheduleValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
