using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoHaveStandard;

public record SetOperationInfoHaveStandardCommand(
    long Id
    ) : ICommand<OperationInfo>;