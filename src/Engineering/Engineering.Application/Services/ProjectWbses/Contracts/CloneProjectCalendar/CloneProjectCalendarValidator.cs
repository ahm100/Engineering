using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;

public class CloneProjectCalendarValidator : AbstractValidator<CloneProjectCalendarRequest>
{
    public CloneProjectCalendarValidator()
    {
        RuleFor(x => x.SourceCalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);

        RuleFor(x => x.TargetProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
