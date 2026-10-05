using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.Transportations.Models.Create;

public record CreateTransportationRequest(
    string TransportationCode,
    string TransportationName,
    bool IsPassenger,
    bool IsActive,
    TransportationType? TransportationType
     ) : IHttpRequest;
