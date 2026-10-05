using Engineering.Application.Services.Branchs.Models.GetBranchByName;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByName;

public record GetBranchByNameQuery(
    string BranchName,
    long CategoryId,
    long? CompanyId)
    : IQuery<GetBranchByNameResponse?>;