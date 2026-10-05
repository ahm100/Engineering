using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToRejected;

public record ChangeStatusToRejectedTransportationRequestCommand(
    long Id,
    string? ManagerDescription,
    long? RejectUserId,
    DateTime? RejectDate
    ) : ICommand<TransportationRequest>;