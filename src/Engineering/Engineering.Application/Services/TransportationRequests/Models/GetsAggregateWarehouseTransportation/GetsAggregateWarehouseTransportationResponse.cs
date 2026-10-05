
namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;

public record GetsAggregateWarehouseTransportationResponse(
    List<GetsAggregateWarehouseTransportationResponseModel> Data,
    int RowCount
    );
