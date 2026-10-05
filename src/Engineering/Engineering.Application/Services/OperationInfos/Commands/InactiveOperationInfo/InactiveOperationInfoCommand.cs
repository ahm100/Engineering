using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.InactiveOperationInfo;

public record InactiveOperationInfoCommand(
    long Id
    ) : ICommand<OperationInfo>;