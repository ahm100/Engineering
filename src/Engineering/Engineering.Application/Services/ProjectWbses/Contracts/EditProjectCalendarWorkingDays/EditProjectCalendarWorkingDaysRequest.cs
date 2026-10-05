namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;

public record EditProjectCalendarWorkingDaysRequest(
    long CalendarId,
    List<EditProjectCalendarWorkingDayItem> WorkingDays) : IHttpRequest;

public record EditProjectCalendarWorkingDayItem(
    DayOfWeek DayOfWeek,
    bool IsWorking,
    List<EditProjectCalendarWorkingTimeItem> WorkingTimes);

public record EditProjectCalendarWorkingTimeItem(
    TimeSpan From,
    TimeSpan To);