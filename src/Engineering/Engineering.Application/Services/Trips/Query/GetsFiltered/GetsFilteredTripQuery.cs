using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Queries.GetsFiltered;

public record GetsFilteredTripQuery(
    List<long>? Ids,
    string? FilterData,
    string? TripName,
    string? TripCode,
    bool? IsActive,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Trip>>>;
