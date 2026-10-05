using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.StateChangerOperationInfos;

public record StateChangerOperationInfosCommand(
    List<OperationInfo> Items,
    bool State
    ) : ICommand<bool?>;
