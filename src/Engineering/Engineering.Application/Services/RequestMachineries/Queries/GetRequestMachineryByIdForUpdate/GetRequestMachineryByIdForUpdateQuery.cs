using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdForUpdate;

public record GetRequestMachineryByIdForUpdateQuery(long RequestMachineryId) : IQuery<RequestMachinery>;

