using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByExperts;

public record GetOperationInfoByIdByExpertsQuery(
    long Id
    ) : IQuery<OperationInfo>;