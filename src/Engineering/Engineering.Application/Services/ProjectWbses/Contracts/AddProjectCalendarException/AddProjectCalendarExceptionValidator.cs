namespace Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

public class AddProjectCalendarExceptionValidator : AbstractValidator<AddProjectCalendarExceptionRequest>
{
    public AddProjectCalendarExceptionValidator()
    {
        RuleFor(x => x.CalendarId)
        .IsPositive(GlobalCmts.Id)
        .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
