namespace Engineering.Application.Services.Branchs.Models.GetBranchByName;

public record GetBranchByNameRequest(
    string BranchName,
    long CategoryId)
    : IHttpRequest;