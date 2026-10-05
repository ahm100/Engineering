namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;

public record EditProjectCalendarDetailsRequest(
    long CalendarId,
    string TitleFa,
    string? TitleEn,
    int MinutesPerDay,
    bool IsDefault) : IHttpRequest;