using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Commands.AddProjectCalendarException;

public class AddProjectCalendarExceptionCommandValidator : AbstractValidator<AddProjectCalendarExceptionCommand>
{
    public AddProjectCalendarExceptionCommandValidator()
    {
        RuleFor(c => c.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(ProjectErrors.UnValidId);

        RuleFor(c => c)
            .Must(c => c.From.HasValue == c.To.HasValue)
            .WithError(ProjectErrors.InvalidCalendarDefinition);

        RuleFor(c => c)
            .Must(c =>
                !c.From.HasValue ||
                !c.To.HasValue ||
                c.From.Value < c.To.Value)
            .WithError(ProjectErrors.WorkingTimeMisMatch);
    }
}
