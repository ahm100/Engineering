
namespace Engineering.Application.Services.Trips.Models.Update;

public record UpdateTripRequest(
    long Id,
    string TripName,
    string TripCode,
    bool IsActive
     ) : IHttpRequest;
