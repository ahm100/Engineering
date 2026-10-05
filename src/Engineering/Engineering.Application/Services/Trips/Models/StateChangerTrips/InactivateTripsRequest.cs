
namespace Engineering.Application.Services.Trips.Models.StateChangerTrips;

public record InactivateTripsRequest(
    List<long> Ids
    ) : IHttpRequest;
