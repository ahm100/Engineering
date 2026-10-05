
namespace Engineering.Application.Services.Trips.Models.GetById;

public record GetTripByIdRequest(
    long Id
     ) : IHttpRequest;
