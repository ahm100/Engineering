using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarDetails;

public class EditProjectCalendarDetailsCommandValidator : AbstractValidator<EditProjectCalendarDetailsCommand>
{
    public EditProjectCalendarDetailsCommandValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
