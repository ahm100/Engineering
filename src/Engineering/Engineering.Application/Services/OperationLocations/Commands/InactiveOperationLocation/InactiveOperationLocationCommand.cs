using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.InactiveOperationLocation;

public record InactiveOperationLocationCommand(
    long Id
    ) : ICommand<OperationLocation>;