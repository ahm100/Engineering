using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectCalendar;

public record CreateProjectCalendarCommand(
    long ProjectId,
    string TitleFa,
    string? TitleEn,
    bool IsDefault,
    int MinutesPerDay,
    List<CreateProjectCalendarWorkingDayRequest> WorkingDays,
    List<CreateProjectCalendarExceptionRequest>? Exceptions) : ICommand<CreateProjectCalendarResponse?>;