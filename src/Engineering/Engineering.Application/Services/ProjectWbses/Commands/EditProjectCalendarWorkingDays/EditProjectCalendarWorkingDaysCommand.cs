using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarWorkingDays;

public record EditProjectCalendarWorkingDaysCommand(
    long CalendarId,
    List<EditProjectCalendarWorkingDayItem> WorkingDays) : ICommand<EditProjectCalendarWorkingDaysResponse?>;