using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoWithProjectOperationId;

public record GetOperationInfoWithProjectOperationIdQuery(
    long Id
    ) : IQuery<OperationInfo>;