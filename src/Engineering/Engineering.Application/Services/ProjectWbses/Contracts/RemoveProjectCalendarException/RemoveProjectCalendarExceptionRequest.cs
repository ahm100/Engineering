namespace Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectCalendarException;

public record RemoveProjectCalendarExceptionRequest(
    long CalendarId,
    long ExceptionId) : IHttpRequest;