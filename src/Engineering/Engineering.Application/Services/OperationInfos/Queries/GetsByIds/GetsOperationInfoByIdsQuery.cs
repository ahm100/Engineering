
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIds;

public record GetsOperationInfoByIdsQuery(
    List<long> Ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;