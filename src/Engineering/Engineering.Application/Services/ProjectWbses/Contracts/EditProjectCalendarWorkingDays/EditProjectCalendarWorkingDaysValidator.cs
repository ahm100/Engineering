namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;

public class EditProjectCalendarWorkingDaysValidator : AbstractValidator<EditProjectCalendarWorkingDaysRequest>
{
    public EditProjectCalendarWorkingDaysValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}