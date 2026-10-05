namespace Engineering.Application.Services.Branchs.Models.DisableBranch;

public record DisableBranchRequest(
    long Id)
    : IHttpRequest;