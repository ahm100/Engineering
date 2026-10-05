using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeCargoToSecurityConfirm;

public record ChangeCargoToSecurityConfirmCommand(
    long Id,
    string? ManagerDescription,
    bool WarehouseStatus = false
    ) : ICommand<TransportationCargo>;