using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateCostCenterRequests;

public record UpdateCostCenterRequestsCommand(
    List<TransportationRequestCostCenter> CostCenters,
    TransportationRequest TransportationRequest
    ) : ICommand<TransportationRequest>;