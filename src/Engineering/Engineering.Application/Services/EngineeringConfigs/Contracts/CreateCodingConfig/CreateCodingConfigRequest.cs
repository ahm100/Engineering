using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Application.Services.EngineeringConfigs.Contracts.CreateCodingConfig;

public record CreateCodingConfigRequest(
    long ConfigId,
    CodingAlgorithmType Type,
    string Prefix,
    bool IsActive
     ) : IHttpRequest;