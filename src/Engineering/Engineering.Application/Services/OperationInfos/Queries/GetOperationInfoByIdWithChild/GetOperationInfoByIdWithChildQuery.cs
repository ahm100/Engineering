using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdWithChild;

public record GetOperationInfoByIdWithChildQuery(
    long Id
    ) : IQuery<OperationInfo>;