namespace Engineering.Application.Services.Trips.Models.Disable;

public record DisableTripRequest(
    long Id
     ) : IHttpRequest;
