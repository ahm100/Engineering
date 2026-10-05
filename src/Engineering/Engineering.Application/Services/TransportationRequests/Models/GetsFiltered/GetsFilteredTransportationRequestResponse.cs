
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;

public record GetsFilteredTransportationRequestResponse(
    List<GetsFilteredTransportationRequestResponseModel> Data,
    int RowCount
    );
