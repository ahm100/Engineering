
namespace Engineering.Application.Services.Transportations.Models.Update;

public record UpdateTransportationResponse(
    long Id,
    string TransportationName,
    string TransportationCode,
    bool IsActive
    );
