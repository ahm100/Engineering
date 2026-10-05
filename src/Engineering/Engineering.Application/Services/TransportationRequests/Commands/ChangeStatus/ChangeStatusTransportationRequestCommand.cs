using Engineering.Domain.Entities.Transportations.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatus;

public record ChangeStatusTransportationRequestCommand(
    long Id,
    TransportationRequestStatus Status,
    string? ManagerDescription,
    bool WarehouseStatus = false
    ) : ICommand<TransportationRequest>;