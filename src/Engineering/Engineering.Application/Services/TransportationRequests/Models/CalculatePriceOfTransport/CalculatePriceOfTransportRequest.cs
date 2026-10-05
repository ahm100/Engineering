namespace Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;

public record CalculatePriceOfTransportRequest(
    long Id,
    decimal LoadWeight
     ) : IHttpRequest;
