using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.HaveBranchChild;

public record HaveBranchChildQuery(
    long Id)
    : IQuery<Branch?>;