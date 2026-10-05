namespace Engineering.Application.Services.ProjectWbses.Commands.ReSchheduledProjectSchedule;

public record ReSchheduledProjectScheduleCommand(
    long Id,
    DateTime DateTime) : ICommand<bool>;