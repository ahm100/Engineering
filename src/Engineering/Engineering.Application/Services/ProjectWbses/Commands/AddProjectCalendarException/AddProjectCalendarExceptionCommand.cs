using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Commands.AddProjectCalendarException;

public record AddProjectCalendarExceptionCommand(
    long CalendarId,
    DateTime Date,
    bool IsWorking,
    string? Description,
    TimeSpan? From,
    TimeSpan? To) : ICommand<AddProjectCalendarExceptionResponse?>;