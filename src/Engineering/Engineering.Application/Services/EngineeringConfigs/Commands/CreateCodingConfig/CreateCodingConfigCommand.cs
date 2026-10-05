using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.CreateCodingConfig;

public record CreateCodingConfigCommand(
    long ConfigId,
    CodingAlgorithmType Type,
    string Prefix,
    bool IsActive
     ) : ICommand<EngineeringCodingConfig?>;