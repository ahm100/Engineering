using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryIds;

public record GetsBranchByCategoryIdsQuery(
    List<long> CategoryIds,
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsBranchByCategoryIdsModel>>>;