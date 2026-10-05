
namespace Engineering.Application.Services.Trips.Models.TripGroupDelete;

public record TripGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
