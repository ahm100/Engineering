
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetsServiceInfoByProjectOperationIds;

public record GetsServiceInfoByProjectOperationIdsQuery(
    List<long>? OperationInfoIds,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ServiceInfo>>>;
