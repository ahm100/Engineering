using Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.CloneProjectCalendar;

public class CloneProjectCalendarCommandValidator : AbstractValidator<CloneProjectCalendarCommand>
{
    public CloneProjectCalendarCommandValidator()
    {
        RuleFor(c => c.SourceCalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);

        RuleFor(c => c.TargetProjectId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);
    }
}
