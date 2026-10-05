using Engineering.Application.Services.ProjectWbses.Contracts.SetDefaultProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.SetDefaultProjectCalendar;

public record SetDefaultProjectCalendarCommand(long CalendarId)
    : ICommand<SetDefaultProjectCalendarResponse?>;