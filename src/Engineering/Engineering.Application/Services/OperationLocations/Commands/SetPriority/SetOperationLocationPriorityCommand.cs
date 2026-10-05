using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.SetPriority;

public record SetOperationLocationPriorityCommand(
    long Id,
    int Priority
    ) : ICommand<OperationLocation>;