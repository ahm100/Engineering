using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchs;

public record GetsBranchsQuery(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    string? BranchName,
    string? BranchCode,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<Branch>>>;