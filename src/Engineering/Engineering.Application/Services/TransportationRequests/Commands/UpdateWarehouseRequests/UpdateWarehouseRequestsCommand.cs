using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseRequests;

public record UpdateWarehouseRequestsCommand(
    List<TransportationRequestWarehouse> Warehouses,
    List<ShippingCost>? ShippingCosts,
    TransportationRequest TransportationRequest
    ) : ICommand<TransportationRequest>;