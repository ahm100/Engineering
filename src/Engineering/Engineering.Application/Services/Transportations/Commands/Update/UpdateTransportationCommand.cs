using Engineering.Domain.Entities.Transportations.Enums;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Update;

public record UpdateTransportationCommand(
    long Id,
    string TransportationName,
    string TransportationCode,
    bool IsPassenger,
    bool IsActive,
    long? CompanyId,
    TransportationType? TransportationType
    ) : ICommand<Transportation>;