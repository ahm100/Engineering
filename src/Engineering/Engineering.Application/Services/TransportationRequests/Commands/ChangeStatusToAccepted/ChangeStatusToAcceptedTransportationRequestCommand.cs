using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToAccepted;

public record ChangeStatusToAcceptedTransportationRequestCommand(
    long Id,
    string? ManagerDescription,
    long? ConfrimUserId,
    DateTime? ConfrimDate,
    bool? PanelPaid
    ) : ICommand<TransportationRequest>;