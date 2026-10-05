using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Inactive;

public record InactiveTripCommand(
    long Id
    ) : ICommand<Trip>;
