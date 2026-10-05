using Engineering.Domain.Entities.Transportations.Enums;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Create;

public record CreateTransportationCommand(
    string TransportationCode,
    string TransportationName,
    bool IsPassenger,
    bool IsActive,
    long? CompanyId,
    TransportationType? TransportationType
    ) : ICommand<Transportation?>;
