using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetById;

public record GetTripByIdQuery(
    long Id
    ) : IQuery<Trip?>;
