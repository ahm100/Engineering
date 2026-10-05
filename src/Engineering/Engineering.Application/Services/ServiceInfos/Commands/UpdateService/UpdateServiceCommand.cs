using Engineering.Domain.Entities.ServiceInfos.Enums;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.UpdateService;

public record UpdateServiceInfoCommand(
    long Id,
    string ServiceInfoCode,
    string ServiceInfoName,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn,
    long UnitOfMeasurementId,
    bool IsActive,
    ServiceInfoType Type,
    List<string>? DocumentUrls,
    long? CompanyId
    ) : ICommand<ServiceInfo>;