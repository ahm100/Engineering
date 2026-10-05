
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServices;

public record GetServiceInfosQuery(
    List<long>? Ids,
    long? OperationInfoId,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    string? ServiceInfoCode,
    string? ServiceInfoName,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ServiceInfo>>>;