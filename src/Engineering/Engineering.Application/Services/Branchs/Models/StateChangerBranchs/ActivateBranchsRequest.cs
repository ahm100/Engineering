namespace Engineering.Application.Services.Branchs.Models.StateChangerBranchs;

public record ActivateBranchsRequest(
    List<long> Ids)
    : IHttpRequest;