namespace Engineering.Application.Services.BillOfLadings.Contracts.CreateBillOfLading;

public record CreateBillOfLadingRequest(
    string BillOfLadingCode,
    string BillOfLadingName,
    bool IsActive)
    : IHttpRequest;