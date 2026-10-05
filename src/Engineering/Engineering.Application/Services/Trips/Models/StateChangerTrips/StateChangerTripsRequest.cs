
namespace Engineering.Application.Services.Trips.Models.StateChangerTrips;

public record StateChangerTripsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
