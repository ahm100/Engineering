using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAfterReforms;

public record UpdateAfterReformsCommand(
    TransportationRequest TransportationRequest,
    TransportationRequestDetail Detail,
    decimal TransferPrice,
    decimal LoadWeight
    ) : ICommand<TransportationRequest>;