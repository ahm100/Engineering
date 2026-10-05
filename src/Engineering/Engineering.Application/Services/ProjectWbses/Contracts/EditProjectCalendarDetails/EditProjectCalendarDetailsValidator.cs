using DocumentFormat.OpenXml.Office2010.Drawing;

namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;

public class EditProjectCalendarDetailsValidator : AbstractValidator<EditProjectCalendarDetailsRequest>
{
    public EditProjectCalendarDetailsValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
