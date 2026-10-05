
namespace Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;

public record ActiveBillOfLadingRequest(
    long Id
    ) : IHttpRequest;
