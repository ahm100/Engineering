using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByMachineries;

public record GetOperationInfoByIdByMachineriesQuery(
    long Id
    ) : IQuery<OperationInfo>;