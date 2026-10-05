using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.StateChangerTrips;

public record StateChangerTripsCommand(
    List<Trip> Items,
    bool State
    ) : ICommand<bool?>;
