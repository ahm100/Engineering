using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetServiceById;

public record GetServiceInfoByIdQuery(
    long Id
    ) : IQuery<ServiceInfo>;