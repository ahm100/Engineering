using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Commands.DeleteOperationInfoGroupRelation;

public record DeleteOperationInfoGroupRelationCommand(
    long OperationInfoGroupId,
    long OprationInfoId
    ) : ICommand<OperationInfoGroupRelation>;
