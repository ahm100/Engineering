using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByGroupRelations;

public record GetOperationInfoByIdByGroupRelationsQuery(
    long Id
    ) : IQuery<OperationInfo>;