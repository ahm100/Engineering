namespace Engineering.Application.Services.Branchs.Models.CreateBranch;

public record CreateBranchRequest(
    long CategoryId,
    string BranchCode,
    string BranchName,
    bool IsActive) : IHttpRequest;