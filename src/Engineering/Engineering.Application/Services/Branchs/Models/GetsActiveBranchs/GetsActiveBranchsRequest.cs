namespace Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;

public record GetsActiveBranchsRequest(
    string? FilterData,
    long? CategoryId,
    string? BranchCode,
    string? BranchName,
    int PageIndex,
    int PageSize) : IHttpRequest;