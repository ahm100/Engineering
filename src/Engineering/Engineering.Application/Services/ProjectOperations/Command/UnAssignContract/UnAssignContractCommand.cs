using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UnAssignContract;

public record UnAssignContractCommand(
    long Id
    ) : ICommand<ProjectOperation>;