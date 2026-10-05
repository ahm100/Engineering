using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.ActiveOperationLocation;

public record ActiveOperationLocationCommand(
    long Id
    ) : ICommand<OperationLocation>;