using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoById;

public record GetOperationInfoByIdQuery(
    long Id
    ) : IQuery<OperationInfo>;