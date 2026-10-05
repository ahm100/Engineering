namespace Engineering.Application.Services.ProjectWbses.Contracts.GetProjectSchedule;

public class GetProjectScheduleValidator : AbstractValidator<GetProjectScheduleRequest>
{
    public GetProjectScheduleValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
