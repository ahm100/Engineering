using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.ActiveOperationInfoGroup;

public record ActiveOperationInfoGroupCommand(
    long Id
    ) : ICommand<OperationInfoGroup>;