using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetByIdForPayment;

public record GetTransportationRequestByIdForPaymentQuery(
    long Id
    ) : IQuery<TransportationRequest?>;
