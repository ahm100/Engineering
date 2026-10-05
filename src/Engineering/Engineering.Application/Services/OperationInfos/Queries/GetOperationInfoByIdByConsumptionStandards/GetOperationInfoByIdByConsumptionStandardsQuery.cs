using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByConsumptionStandards;

public record GetOperationInfoByIdByConsumptionStandardsQuery(
    long Id
    ) : IQuery<OperationInfo>;