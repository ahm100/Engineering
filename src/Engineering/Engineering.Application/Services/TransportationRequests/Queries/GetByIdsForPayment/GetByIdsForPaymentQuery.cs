using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdsForPayment;

public record GetByIdsForPaymentQuery(
    List<long> Ids
    ) : IQuery<List<TransportationRequest>?>;
