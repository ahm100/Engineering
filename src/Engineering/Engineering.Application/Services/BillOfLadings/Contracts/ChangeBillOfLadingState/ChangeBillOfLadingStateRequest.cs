namespace Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;

public record ChangeBillOfLadingStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
