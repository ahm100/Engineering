namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;

public record CreateProjectCalendarRequest(
    long ProjectId,
    string TitleFa,
    string? TitleEn,
    bool IsDefault,
    int MinutesPerDay,
    List<CreateProjectCalendarWorkingDayRequest> WorkingDays,
    List<CreateProjectCalendarExceptionRequest>? Exceptions) : IHttpRequest;

public record CreateProjectCalendarWorkingTimeRequest(
    TimeSpan From,
    TimeSpan To);

public record CreateProjectCalendarWorkingDayRequest(
    DayOfWeek DayOfWeek,
    bool IsWorking,
    List<CreateProjectCalendarWorkingTimeRequest> WorkingTimes);

public record CreateProjectCalendarExceptionRequest(
    DateTime Date,
    bool IsWorking,
    string? Description,
    TimeSpan? From,
    TimeSpan? To);
