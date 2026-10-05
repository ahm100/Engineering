namespace Engineering.Application.Services.Branchs.Models.InactiveBranch;

public record InactiveBranchRequest(
    long Id)
    : IHttpRequest;