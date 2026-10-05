namespace Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

public record AddProjectCalendarExceptionRequest(
    long CalendarId,
    DateTime Date,
    bool IsWorking,
    string? Description,
    TimeSpan? From,
    TimeSpan? To) : IHttpRequest;