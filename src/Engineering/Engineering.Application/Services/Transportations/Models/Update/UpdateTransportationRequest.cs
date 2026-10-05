
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.Transportations.Models.Update;

public record UpdateTransportationRequest(
    long Id,
    string TransportationName,
    string TransportationCode,
    bool IsPassenger,
    bool IsActive,
    TransportationType? TransportationType
     ) : IHttpRequest;
