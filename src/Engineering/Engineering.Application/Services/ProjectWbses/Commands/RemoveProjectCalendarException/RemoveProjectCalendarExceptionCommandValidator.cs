namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectCalendarException;

public class RemoveProjectCalendarExceptionCommandValidator : AbstractValidator<RemoveProjectCalendarExceptionCommand>
{
    public RemoveProjectCalendarExceptionCommandValidator()
    {
        RuleFor(x => x.CalendarId)
            .IsPositive(GlobalCmts.Id)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
