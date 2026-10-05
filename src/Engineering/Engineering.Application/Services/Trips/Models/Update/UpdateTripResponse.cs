
namespace Engineering.Application.Services.Trips.Models.Update;

public record UpdateTripResponse(
    long Id,
    string TripName,
    string TripCode,
    bool IsActive
    );
