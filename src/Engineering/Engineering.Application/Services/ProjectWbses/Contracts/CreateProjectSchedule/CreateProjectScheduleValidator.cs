using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;

public class CreateProjectScheduleValidator : AbstractValidator<CreateProjectScheduleRequest>
{
    public CreateProjectScheduleValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(x => x.ScheduleStartDate)
            .NotEmpty()
            .WithError(GlobalErrors.InValidRequest);
    }
}