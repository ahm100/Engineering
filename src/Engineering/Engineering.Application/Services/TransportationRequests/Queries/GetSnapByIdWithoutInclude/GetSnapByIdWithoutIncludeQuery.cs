using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetSnapByIdWithoutInclude;

public record GetSnapByIdWithoutIncludeQuery(
    long Id
    ) : IQuery<GetSnapByIdResponse?>;
