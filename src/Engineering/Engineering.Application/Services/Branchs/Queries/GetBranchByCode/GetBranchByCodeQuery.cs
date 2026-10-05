using Engineering.Application.Services.Branchs.Models.GetBranchByCode;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByCode;

public record GetBranchByCodeQuery(
    string BranchCode,
    long CategoryId,
    long? CompanyId)
    : IQuery<GetBranchByCodeResponse?>;