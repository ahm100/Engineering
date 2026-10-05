using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.InactiveOperationInfoGroup;

public record InactiveOperationInfoGroupCommand(
    long Id
    ) : ICommand<OperationInfoGroup>;