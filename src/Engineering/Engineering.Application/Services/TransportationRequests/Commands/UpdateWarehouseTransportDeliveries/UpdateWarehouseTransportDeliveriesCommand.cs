using Engineering.ClientSdk.Enums;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportDeliveries;

public record UpdateWarehouseTransportDeliveriesCommand(
    TransportationRequest TransportationRequest,
    DeliveryType? DeliveryType,
    DeliveryMethod? DeliveryMethod,
    DateTime? PostageDate,
    string? Description
    ) : ICommand<TransportationRequest>;