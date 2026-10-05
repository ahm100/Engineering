using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdIncludeLess;

public record GetOperationInfoByIdIncludeLessQuery(
    long Id
    ) : IQuery<OperationInfo>;