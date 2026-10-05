using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;

public record GetRequestMachineryByIdQuery(long RequestMachineryId) : IQuery<RequestMachinery>;

