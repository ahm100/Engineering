using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportExtras;

public record UpdateTransportExtrasCommand(
    TransportationRequest TransportationRequest,
    TransportationRequestDetail Detail,
    decimal TransferPrice,
    decimal? Volume
    ) : ICommand<TransportationRequest>;