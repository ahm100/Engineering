
namespace Engineering.Application.Services.Trips.Models.GetsFiltered;

public record GetsFilteredTripRequest(
    string? FilterData,
    string? TripName,
    string? TripCode,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
