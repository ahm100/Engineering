using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using OperationInfoDependency = Engineering.Domain.Entities.OperationInfos.OperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies.Commands.CreateOperationInfoDependency;

public record CreateOperationInfoDependencyCommand(
    OperationInfo OperationInfo,
    long RelationId,
    int WorkingDays,
    OperationInfoDependencyType DependencyType
    ) : ICommand<OperationInfoDependency>;