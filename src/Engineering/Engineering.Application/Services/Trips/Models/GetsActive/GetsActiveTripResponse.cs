
namespace Engineering.Application.Services.Trips.Models.GetsActive;

public record GetsActiveTripResponse(
    List<GetsActiveTripResponseModel> Data,
    int RowCount
    );
