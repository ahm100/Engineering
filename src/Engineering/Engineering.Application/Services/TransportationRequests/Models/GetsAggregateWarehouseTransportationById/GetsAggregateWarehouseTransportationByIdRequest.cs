namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;

public record GetsAggregateWarehouseTransportationByIdRequest(
    long Id
     ) : IHttpRequest;
