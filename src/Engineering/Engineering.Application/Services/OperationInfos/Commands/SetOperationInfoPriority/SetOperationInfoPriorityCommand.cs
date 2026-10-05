using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoPriority;

public record SetOperationInfoPriorityCommand(
    long Id,
    int? SetPriority
    ) : ICommand<OperationInfo>;