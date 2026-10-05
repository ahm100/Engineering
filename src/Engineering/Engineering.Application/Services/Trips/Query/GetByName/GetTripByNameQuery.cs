using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByName;

public record GetTripByNameQuery(
    string TripName,
    long? CompanyId
    ) : IQuery<Trip?>;
