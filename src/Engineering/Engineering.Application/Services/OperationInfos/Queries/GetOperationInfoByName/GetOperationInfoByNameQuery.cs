using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByName;

public record GetOperationInfoByNameQuery(
    string OperationInfoName,
    long? UnitOfMeasurementId,
    long? CompanyId
    ) : IQuery<OperationInfo>;