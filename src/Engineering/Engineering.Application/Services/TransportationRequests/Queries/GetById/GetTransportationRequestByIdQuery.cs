using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetById;

public record GetTransportationRequestByIdQuery(
    long Id
    ) : IQuery<TransportationRequest?>;
