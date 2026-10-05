
namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;

public record GetsWarehouseTransportationResponse(
    List<GetsWarehouseTransportationResponseModel> Data,
    int RowCount
    );
