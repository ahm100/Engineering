using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Disable;

public record DisableTripCommand(
    long Id
    ) : ICommand<Trip>;
