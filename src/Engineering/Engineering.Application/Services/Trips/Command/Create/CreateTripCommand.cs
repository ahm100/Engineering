using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Create;

public record CreateTripCommand(
    string TripCode,
    string TripName,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Trip?>;
