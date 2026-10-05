namespace Engineering.Application.Services.Branchs.Models.GetsBranchs;

public record GetsBranchsRequest(
    string? FilterData,
    long? CategoryId,
    string? BranchName,
    string? BranchCode,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;