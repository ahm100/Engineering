namespace Engineering.Application.Services.Branchs.Models.UpdateBranch;

public record UpdateBranchRequest(
    long Id,
    long CategoryId,
    string BranchName,
    string BranchCode,
    bool IsActive)
    : IHttpRequest;