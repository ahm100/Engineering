using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.DisableOperationInfoGroup;

public record DisableOperationInfoGroupCommand(
    long Id
    ) : ICommand<OperationInfoGroup>;