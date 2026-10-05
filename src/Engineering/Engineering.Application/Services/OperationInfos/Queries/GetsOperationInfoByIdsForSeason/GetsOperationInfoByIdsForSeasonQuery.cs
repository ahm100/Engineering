
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsForSeason;

public record GetsOperationInfoByIdsForSeasonQuery(
    List<long> Ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;