namespace Engineering.Application.Services.Trips.Models.GetByCode;

public record GetTripByCodeRequest(
    string TripCode
     ) : IHttpRequest;
