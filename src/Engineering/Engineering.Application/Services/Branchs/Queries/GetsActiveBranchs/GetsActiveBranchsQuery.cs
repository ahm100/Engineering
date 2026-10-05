using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;

namespace Engineering.Application.Services.Branchs.Queries.GetsActiveBranchs;

public record GetsActiveBranchsQuery(
    string? FilterData,
    long? CategoryId,
    string? BranchCode,
    string? BranchName,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsActiveBranchsResponseModel>>>;