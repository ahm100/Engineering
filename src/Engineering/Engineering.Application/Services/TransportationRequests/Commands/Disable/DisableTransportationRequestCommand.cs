using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.Disable;

public record DisableTransportationRequestCommand(
    long Id
    ) : ICommand<TransportationRequest>;