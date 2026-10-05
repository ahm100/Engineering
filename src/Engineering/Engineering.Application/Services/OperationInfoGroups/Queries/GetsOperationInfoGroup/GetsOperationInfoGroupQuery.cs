using OperationInfoGroup = Engineering.Domain.Entities.OperationInfos.OperationInfoGroup;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroup;

public record GetsOperationInfoGroupQuery(
    List<long>? Ids,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoGroup>>>;