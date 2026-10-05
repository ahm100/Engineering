
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;

public record GetsFilteredTransportationCargoResponse(
    List<GetsFilteredTransportationCargoResponseModel> Data,
    int RowCount
    );
