using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByName;

public record GetOperationInfoGroupByNameQuery(
    string OperationInfoGroupName,
    long? CompanyId
    ) : IQuery<OperationInfoGroup?>;
