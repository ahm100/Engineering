namespace Engineering.Application.Services.Transportations.Models.Create;

public record CreateTransportationResponse(
    long Id,
    string TransportationCode,
    string TransportationName,
    bool IsActive
    );
