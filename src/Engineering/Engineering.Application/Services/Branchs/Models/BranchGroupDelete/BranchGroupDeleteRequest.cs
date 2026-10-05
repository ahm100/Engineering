namespace Engineering.Application.Services.Branchs.Models.BranchGroupDelete;

public record BranchGroupDeleteRequest(
    List<long> Ids)
    : IHttpRequest;