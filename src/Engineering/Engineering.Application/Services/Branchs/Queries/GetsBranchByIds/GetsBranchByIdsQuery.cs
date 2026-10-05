using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByIds;

public record GetsBranchByIdsQuery(
    List<long> Items)
    : IQuery<List<Branch>>;