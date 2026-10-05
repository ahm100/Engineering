namespace Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;

public record UpdateBillOfLadingRequest(
    long Id,
    string BillOfLadingName,
    string BillOfLadingCode,
    bool IsActive)
    : IHttpRequest;