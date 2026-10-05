
namespace Engineering.Application.Services.Trips.Models.GetsFiltered;

public record GetsFilteredTripResponse(
    List<GetsFilteredTripResponseModel> Data,
    int RowCount
    );
