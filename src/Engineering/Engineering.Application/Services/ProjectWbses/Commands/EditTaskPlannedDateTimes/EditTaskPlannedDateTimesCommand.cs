using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskPlannedDateTimes;

public record EditTaskPlannedDateTimesCommand(
    long Id,
    DateTime DateTime,
    bool IsStart) : ICommand<ProjectScheduleTask>;