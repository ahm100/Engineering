using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateCodingConfig;

public record UpdateCodingConfigRequest(
    long Id,
    long ConfigId,
    CodingAlgorithmType Type,
    string Prefix,
    bool IsActive
     ) : IHttpRequest;