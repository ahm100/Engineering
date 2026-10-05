using Engineering.Domain.Entities.Transportations;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetPaidByDate;

public record GetsTransportationRequestsByRequestIdQuery(long RequestById,
                                                         DateTime StartDate,
                                                         DateTime EndDate,
                                                         TransportationRequestStatus Status) : IQuery<List<TransportationRequest>>;