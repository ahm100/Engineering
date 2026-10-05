using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroupByIds;

public record GetsOperationInfoGroupByIdsQuery(
    List<long> GroupIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoGroup>>>;