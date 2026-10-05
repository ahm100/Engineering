using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Commands.StateChangerOperationInfoGroups;

public record StateChangerOperationInfoGroupsCommand(
    List<OperationInfoGroup> Items,
    bool State
    ) : ICommand<bool?>;
