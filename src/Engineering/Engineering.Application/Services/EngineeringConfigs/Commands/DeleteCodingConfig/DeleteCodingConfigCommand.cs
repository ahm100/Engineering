using Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteCodingConfig;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.DeleteCodingConfig;

public record DeleteCodingConfigCommand(
    long Id
     ) : ICommand<DeleteCodingConfigResponse?>;