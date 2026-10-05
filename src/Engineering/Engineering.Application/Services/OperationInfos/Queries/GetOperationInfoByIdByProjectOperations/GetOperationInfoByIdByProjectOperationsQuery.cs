using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProjectOperations;

public record GetOperationInfoByIdByProjectOperationsQuery(
    long Id
    ) : IQuery<OperationInfo>;