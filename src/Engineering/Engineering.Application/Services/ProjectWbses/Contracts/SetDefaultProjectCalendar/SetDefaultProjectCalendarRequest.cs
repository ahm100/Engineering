namespace Engineering.Application.Services.ProjectWbses.Contracts.SetDefaultProjectCalendar;

public record SetDefaultProjectCalendarRequest(
    long CalendarId) : IHttpRequest;