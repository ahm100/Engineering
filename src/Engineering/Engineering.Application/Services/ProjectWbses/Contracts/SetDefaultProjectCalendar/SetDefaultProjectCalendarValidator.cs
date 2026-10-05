namespace Engineering.Application.Services.ProjectWbses.Contracts.SetDefaultProjectCalendar;

public class SetDefaultProjectCalendarValidator : AbstractValidator<SetDefaultProjectCalendarRequest>
{
    public SetDefaultProjectCalendarValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
