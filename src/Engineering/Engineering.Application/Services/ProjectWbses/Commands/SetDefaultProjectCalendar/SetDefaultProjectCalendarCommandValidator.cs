namespace Engineering.Application.Services.ProjectWbses.Commands.SetDefaultProjectCalendar;

public class SetDefaultProjectCalendarCommandValidator : AbstractValidator<SetDefaultProjectCalendarCommand>
{
    public SetDefaultProjectCalendarCommandValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
