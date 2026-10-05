using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Active;

public record ActiveTripCommand(
    long Id
    ) : ICommand<Trip>;
