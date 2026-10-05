using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.DisableOperationInfo;

public record DisableOperationInfoCommand(
    long Id
    ) : ICommand<OperationInfo>;