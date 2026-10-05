using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCodes;

public record GetsBranchByCodesQuery(
    List<string> Codes,
    long CategoryId,
    long? CompanyId)
    : IQuery<DataResult<List<Branch>>>;