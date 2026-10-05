using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByCode;

public record GetTripByCodeQuery(
    string TripCode,
    long? CompanyId
    ) : IQuery<Trip?>;
