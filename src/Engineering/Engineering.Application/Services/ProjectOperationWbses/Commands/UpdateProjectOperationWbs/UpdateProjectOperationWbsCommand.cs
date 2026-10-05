using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.UpdateProjectOperationWbs;

public record UpdateProjectOperationWbsCommand(
    long Id,
    long? ProjectWbsId,
    long? ProjectOperationId,
    bool? IsActive) : ICommand<ProjectOperationWbs?>;