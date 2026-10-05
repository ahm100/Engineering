using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Update;

public record UpdateTripCommand(
    long Id,
    string TripName,
    string TripCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<Trip>;
