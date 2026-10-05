using Engineering.Domain.Entities.Transportations;
using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentTransportationStatusChange;

public record PaymentTransportationStatusChangeCommand(
    long RefrenceId,
    PaymentOrderStatus? Status,
    bool IsDeleted
    ) : ICommand<TransportationRequest>;
