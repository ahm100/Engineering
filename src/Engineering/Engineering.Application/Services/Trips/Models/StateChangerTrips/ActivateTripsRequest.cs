
namespace Engineering.Application.Services.Trips.Models.StateChangerTrips;

public record ActivateTripsRequest(
    List<long> Ids
    ) : IHttpRequest;
