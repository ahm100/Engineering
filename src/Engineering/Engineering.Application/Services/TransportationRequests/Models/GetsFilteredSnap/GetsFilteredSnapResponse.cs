
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;

public record GetsFilteredSnapResponse(
    List<GetsFilteredSnapResponseModel> Data,
    int RowCount
    );
