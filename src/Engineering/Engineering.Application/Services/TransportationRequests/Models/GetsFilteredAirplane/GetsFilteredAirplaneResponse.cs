
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;

public record GetsFilteredAirplaneResponse(
    List<GetsFilteredAirplaneResponseModel> Data,
    int RowCount
    );
