
namespace Engineering.Application.Services.TransportationRequests.Models.GetById;

public record GetTransportationRequestByIdRequest(
    long Id
     ) : IHttpRequest;
