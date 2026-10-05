using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectCalendarException;

public record RemoveProjectCalendarExceptionCommand(
    long CalendarId,
    long ExceptionId) : ICommand<RemoveProjectCalendarExceptionResponse?>;