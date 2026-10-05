using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetAirPlaneByIdWithoutInclude;

public record GetAirPlaneByIdWithoutIncludeQuery(
    long Id
    ) : IQuery<GetAirplaneByIdResponse?>;
