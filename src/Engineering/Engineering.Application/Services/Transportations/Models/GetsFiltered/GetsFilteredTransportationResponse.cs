
namespace Engineering.Application.Services.Transportations.Models.GetsFiltered;

public record GetsFilteredTransportationResponse(
    List<GetsFilteredTransportationResponseModel> Data,
    int RowCount
    );
