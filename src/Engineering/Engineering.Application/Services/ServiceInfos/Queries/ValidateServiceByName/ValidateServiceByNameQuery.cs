using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.ValidateServiceByName;

public record ValidateServiceByNameQuery(
    string ServiceInfoName,
    long MeasurementId,
    long? CompanyId
    ) : IQuery<ServiceInfo>;