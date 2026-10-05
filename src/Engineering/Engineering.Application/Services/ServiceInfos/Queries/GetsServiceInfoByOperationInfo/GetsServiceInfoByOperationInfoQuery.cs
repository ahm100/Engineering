
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetsServiceInfoByOperationInfo;

public record GetsServiceInfoByOperationInfoQuery(
    List<long>? OperationInfoIds,
    string? FilterData,
    bool? IsActive,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ServiceInfo>>>;
