using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;

public record GetsServiceInfoByIdsQuery(
    int PageIndex,
    int PageSize,
    List<long> Ids
    ) : IQuery<DataResult<List<ServiceInfo>>>;