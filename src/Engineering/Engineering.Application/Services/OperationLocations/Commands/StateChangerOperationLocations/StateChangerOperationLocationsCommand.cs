using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.StateChangerOperationLocations;

public record StateChangerOperationLocationsCommand(
    List<OperationLocation> Items,
    bool State
    ) : ICommand<bool?>;
