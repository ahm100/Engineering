namespace Engineering.Application.Services.Branchs.Models.GetBranchByCode;

public record GetBranchByCodeRequest(
    string BranchCode,
    long CategoryId)
    : IHttpRequest;