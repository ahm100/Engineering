using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetByCodes;

public record GetTripByCodesQuery(
    List<string> TripCodes,
    long? CompanyId
    ) : IQuery<List<Trip>?>;
