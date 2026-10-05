
namespace Engineering.Application.Services.Trips.Models.GetsActive;

public record GetsActiveTripRequest(
    string? FilterData,
    string? TripCode,
    string? TripName,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
