using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByCode;

public record GetServiceInfoByCodeQuery(
    string ServiceInfoCode,
    long? CompanyId
    ) : IQuery<ServiceInfo>;