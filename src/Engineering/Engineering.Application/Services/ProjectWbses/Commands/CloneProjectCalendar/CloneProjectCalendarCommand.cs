using Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.CloneProjectCalendar;

public record CloneProjectCalendarCommand(
    long TargetProjectId,
    long SourceCalendarId,
    string? NewTitleFa,
    bool SetAsDefault) : ICommand<CloneProjectCalendarResponse?>;