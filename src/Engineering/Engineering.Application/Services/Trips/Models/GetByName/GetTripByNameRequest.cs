
namespace Engineering.Application.Services.Trips.Models.GetByName;

public record GetTripByNameRequest(
    string TripName
     ) : IHttpRequest;
