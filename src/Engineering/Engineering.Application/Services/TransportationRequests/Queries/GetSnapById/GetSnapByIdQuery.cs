using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapById;

public record GetSnapByIdQuery(
    long Id
    ) : IQuery<TransportationRequest?>;
