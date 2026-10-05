namespace Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;

public record CloneProjectCalendarRequest(
    long TargetProjectId,
    long SourceCalendarId,
    string? NewTitleFa,
    bool SetAsDefault) : IHttpRequest;