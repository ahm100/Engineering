namespace Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;

public record InActiveBillOfLadingRequest(
    long Id
    ) : IHttpRequest;
