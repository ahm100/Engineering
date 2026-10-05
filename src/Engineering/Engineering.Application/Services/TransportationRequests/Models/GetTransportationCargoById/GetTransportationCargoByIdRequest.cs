namespace Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;

public record GetTransportationCargoByIdRequest(
    long Id
     ) : IHttpRequest;
