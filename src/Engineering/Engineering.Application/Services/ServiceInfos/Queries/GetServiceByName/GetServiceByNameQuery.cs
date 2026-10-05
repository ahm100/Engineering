using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceByName;

public record GetServiceInfoByNameQuery(
    string ServiceInfoName,
    long? CompanyId
    ) : IQuery<ServiceInfo>;