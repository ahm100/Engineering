using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchById;

public record GetBranchByIdQuery(
    long Id)
    : IQuery<Branch?>;