namespace Engineering.Application.Services.Branchs.Models.StateChangerBranchs;

public record InactivateBranchsRequest(
    List<long> Ids)
    : IHttpRequest;