
namespace Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

public record GetAirplaneByIdRequest(
    long Id
     ) : IHttpRequest;
