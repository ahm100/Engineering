
namespace Engineering.Application.Services.Transportations.Models.GetsActive;

public record GetsActiveTransportationResponse(
    List<GetsActiveTransportationResponseModel> Data,
    int RowCount
    );

