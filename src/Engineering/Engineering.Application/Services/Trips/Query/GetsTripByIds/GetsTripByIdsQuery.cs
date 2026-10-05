using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsTripByIds;

public record GetsTripByIdsQuery(
    List<long> Items
    ) : IQuery<List<Trip>>;
