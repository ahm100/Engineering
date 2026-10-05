using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectTasksPredecessors;

public record EditProjectTasksPredecessorsCommand(
    long Id,
    string Predecessors) : ICommand<ProjectScheduleTask?>;