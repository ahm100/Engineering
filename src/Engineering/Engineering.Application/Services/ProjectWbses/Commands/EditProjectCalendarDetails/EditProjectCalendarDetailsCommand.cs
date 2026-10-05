using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarDetails;

public record EditProjectCalendarDetailsCommand(
    long CalendarId,
    string TitleFa,
    string? TitleEn,
    int MinutesPerDay,
    bool IsDefault) : ICommand<EditProjectCalendarDetailsResponse?>;