using Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateCodingConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.UpdateCodingConfig;

public record UpdateCodingConfigCommand(
    long Id,
    long ConfigId,
    CodingAlgorithmType Type,
    string Prefix,
    bool IsActive
     ) : ICommand<UpdateCodingConfigResponse?>;
