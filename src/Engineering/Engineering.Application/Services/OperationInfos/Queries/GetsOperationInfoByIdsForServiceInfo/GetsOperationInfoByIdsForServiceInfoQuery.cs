
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsForServiceInfo;

public record GetsOperationInfoByIdsForServiceInfoQuery(
    List<long> Ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfo>>>;