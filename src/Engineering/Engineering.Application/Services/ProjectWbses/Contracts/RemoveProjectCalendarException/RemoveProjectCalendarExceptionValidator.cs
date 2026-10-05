namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectCalendarException;

public class RemoveProjectCalendarExceptionValidator : AbstractValidator<RemoveProjectCalendarExceptionRequest>
{
    public RemoveProjectCalendarExceptionValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
