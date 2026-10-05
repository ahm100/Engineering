using Engineering.Application.Services.TransportationRequests.Models.GetById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetTransportationRequestByIdWithoutInclude;

public record GetTransportationRequestByIdWithoutIncludeQuery(
    long Id
    ) : IQuery<GetTransportationRequestByIdResponse?>;
