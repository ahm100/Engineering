using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.CreateProjectOperationWbs;

public record CreateProjectOperationWbsCommand(
    long ProjectWbsId,
    List<long> ProjectOperationIds,
    bool IsActive) : ICommand<List<ProjectOperationWbs>?>;