using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIds;

public record GetRequestMachineryByIdsQuery(List<long> RequestMachineryIds) : IQuery<List<RequestMachinery>>;

