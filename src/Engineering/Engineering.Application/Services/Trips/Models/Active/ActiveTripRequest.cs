namespace Engineering.Application.Services.Trips.Models.Active;

public record ActiveTripRequest(
    long Id
     ) : IHttpRequest;
