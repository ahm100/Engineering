using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.DisableOperationLocation;

public record DisableOperationLocationCommand(
    long Id
    ) : ICommand<OperationLocation>;