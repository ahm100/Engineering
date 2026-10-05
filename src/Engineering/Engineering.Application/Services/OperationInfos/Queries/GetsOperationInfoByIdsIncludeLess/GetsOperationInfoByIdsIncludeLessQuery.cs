using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsIncludeLess;

public record GetsOperationInfoByIdsIncludeLessQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<OperationInfo>>>;