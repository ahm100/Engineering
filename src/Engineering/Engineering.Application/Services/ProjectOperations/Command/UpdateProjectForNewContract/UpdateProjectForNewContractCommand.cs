using Engineering.Domain.Entities.Projects;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectForNewContract;

public record UpdateProjectForNewContractCommand(
    ProjectOperation ProjectOperation,
    Project Project
    ) : ICommand<ProjectOperation>;