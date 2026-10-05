using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.CreateOperationInfoGroupRelation;

public record CreateOperationInfoGroupRelationCommand(
    OperationInfo OperationInfo,
    List<OperationInfoGroup> OperationInfoGroups
    ) : ICommand<List<OperationInfoGroupRelation>?>;
