using Engineering.Domain.Entities.Transportations.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportationRequestAfterPaymentCommand;

public record UpdateTransportationRequestAfterPaymentCommand(
    TransportationRequest TransportationRequest,
    long? PaymentOrderId,
    string? Description,
    DateTime? PaymentDate,
    TransportationPaymentType? PaymentType
    ) : ICommand<TransportationRequest>;