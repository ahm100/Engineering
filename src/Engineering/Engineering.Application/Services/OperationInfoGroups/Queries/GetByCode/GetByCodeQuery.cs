using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByCode;

public record GetOperationInfoGroupByCodeQuery(
    string OperationInfoGroupCode,
    long? CompanyId
    ) : IQuery<OperationInfoGroup?>;