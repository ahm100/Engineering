using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditPhysicalPercentComplete;

public record EditPhysicalPercentCompleteCommand(
    long Id,
    decimal? Percent) : ICommand<ProjectScheduleTask?>;