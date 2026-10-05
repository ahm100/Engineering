using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByCode;

public record GetOperationInfoByCodeQuery(
    string OperationInfoCode,
    long? CompanyId
    ) : IQuery<OperationInfo>;