
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetActiveServices;

public record GetActiveServiceInfosQuery(
    string? FilterData,
    string? ServiceInfoCode,
    string? ServiceInfoName,
    long? ProjectId,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ServiceInfo>>>;