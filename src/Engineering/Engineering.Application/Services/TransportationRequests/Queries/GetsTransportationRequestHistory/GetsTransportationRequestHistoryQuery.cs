using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTransportationRequestHistory;

public record GetsTransportationRequestHistoryQuery(
    long Id
    ) : IQuery<GetsTransportationRequestHistoryResponse>;
