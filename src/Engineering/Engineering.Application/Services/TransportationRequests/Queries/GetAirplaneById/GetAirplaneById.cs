using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirplaneById;

public record GetAirplaneByIdQuery(
    long Id
    ) : IQuery<TransportationRequest?>;
