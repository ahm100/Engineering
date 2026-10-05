using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportLoadWeight;

public record UpdateTransportLoadWeightCommand(
    TransportationRequest TransportationRequest,
    decimal? LoadWeight,
    bool UpdatePallets = false
    ) : ICommand<TransportationRequest>;