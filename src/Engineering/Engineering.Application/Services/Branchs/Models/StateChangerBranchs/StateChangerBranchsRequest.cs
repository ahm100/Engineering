namespace Engineering.Application.Services.Branchs.Models.StateChangerBranchs;

public record StateChangerBranchsRequest(
    List<long> Ids,
    bool State)
    : IHttpRequest;