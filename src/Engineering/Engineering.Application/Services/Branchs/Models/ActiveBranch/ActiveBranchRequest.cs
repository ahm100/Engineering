namespace Engineering.Application.Services.Branchs.Models.ActiveBranch;

public record ActiveBranchRequest(
    long Id)
    : IHttpRequest;