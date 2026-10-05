namespace Engineering.Application.Services.Trips.Models.Create;

public record CreateTripResponse(
    long Id,
    string TripCode,
    string TripName,
    bool IsActive
    );
