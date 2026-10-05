namespace Engineering.Application.Services.Trips.Models.Create;

public record CreateTripRequest(
    string TripCode,
    string TripName,
    bool IsActive
     ) : IHttpRequest;
