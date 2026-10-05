using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.DisableOperationInfoDependency;

public record DisableOperationInfoDependencyCommand(
    long Id
    ) : ICommand<OperationInfoDependency>;