using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchWithoutInclude;

public record GetBranchWithoutIncludeQuery(
    long Id)
    : IQuery<Branch?>;