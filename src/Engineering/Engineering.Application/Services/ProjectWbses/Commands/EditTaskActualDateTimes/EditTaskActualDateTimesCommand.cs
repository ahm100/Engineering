using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskActualDateTimes;

public record EditTaskActualDateTimesCommand(
    long TaskId,
    DateTime? DateTime,
    bool IsStart) : ICommand<ProjectScheduleTask?>;