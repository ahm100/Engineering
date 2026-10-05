using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPercentComplete;

public record EditPercentCompleteCommand(
    long Id,
    decimal? Percent) : ICommand<ProjectScheduleTask?>;