using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsActive;

public record GetsActiveTripQuery(
    string? FilterData,
    string? TripCode,
    string? TripName,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Trip>>>;
