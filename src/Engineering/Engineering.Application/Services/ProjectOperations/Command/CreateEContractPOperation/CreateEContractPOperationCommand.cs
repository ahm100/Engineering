using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Commands.CreateEContractPOperation;

public record CreateEContractPOperationCommand(
    EmployerContract EmployerContract,
    List<OperationInfo> OperationInfos,
    List<EmployerConsideration>? FlagedConsiderations,
    List<CreateProjectOperationModel> CreatePOperations
    ) : ICommand<List<ProjectOperation>>;