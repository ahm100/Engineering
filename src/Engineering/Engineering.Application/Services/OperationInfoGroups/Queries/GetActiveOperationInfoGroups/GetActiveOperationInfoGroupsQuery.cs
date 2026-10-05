using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetActiveOperationInfoGroups;

public record GetActiveOperationInfoGroupsQuery(
    string? FilterData,
    string? code,
    string? name,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoGroup>>>;