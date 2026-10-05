using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.UpdateOperationInfoDependency;

public record UpdateOperationInfoDependencyCommand(
    long Id,
    OperationInfo OperationInfo,
    int WorkingDays,
    OperationInfoDependencyType DependencyType
    ) : ICommand<OperationInfoDependency>;