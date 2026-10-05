using Engineering.Domain.Entities.EmployerContracts;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.AssignContract;

public record AssignContractCommand(
    ProjectOperation ProjectOperation,
    EmployerContract EmployerContract
    ) : ICommand<ProjectOperation>;