using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.ActiveOperationInfo;

public record ActiveOperationInfoCommand(
    long Id
    ) : ICommand<OperationInfo>;