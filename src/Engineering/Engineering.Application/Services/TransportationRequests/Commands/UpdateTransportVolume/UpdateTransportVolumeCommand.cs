using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportVolume;

public record UpdateTransportVolumeCommand(
    TransportationRequest TransportationRequest,
    decimal? Volume
    ) : ICommand<TransportationRequest>;