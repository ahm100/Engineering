using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarWorkingDays;

public class EditProjectCalendarWorkingDaysCommandValidator : AbstractValidator<EditProjectCalendarWorkingDaysCommand>
{
    public EditProjectCalendarWorkingDaysCommandValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
