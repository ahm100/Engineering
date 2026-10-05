using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos.Enums;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.CreateService;

public record CreateServiceInfoCommand(
    string ServiceInfoCode,
    string ServiceInfoName,
    string? ServiceInfoEnName,
    string? DescriptionFa,
    string? DescriptionEn,
    long UnitOfMeasurementId,
    List<OperationInfo>? OperationInfos,
    long TimeSpant,
    bool IsActive,
    ServiceInfoType Type,
    List<string>? DocumentUrls,
    long? CompanyId
    ) : ICommand<ServiceInfo>;