using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduleColumn;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduleColumn;

public record RemoveProjectScheduleColumnCommand(
    long Id) : ICommand<RemoveProjectScheduleColumnResponse>;