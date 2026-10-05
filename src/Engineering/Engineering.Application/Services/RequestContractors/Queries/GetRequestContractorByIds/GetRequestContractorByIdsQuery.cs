using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorByIds;

public record GetRequestContractorByIdsQuery(List<long> RequestContractorIds) : IQuery<List<RequestContractor>>;

