namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportLoadWeight;

public record UpdateTransportLoadWeightRequest(
    long Id,
    decimal LoadWeight
     ) : IHttpRequest;
