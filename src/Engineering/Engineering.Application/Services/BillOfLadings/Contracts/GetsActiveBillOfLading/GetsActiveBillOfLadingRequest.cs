namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;

public record GetsActiveBillOfLadingRequest(
    string? FilterData,
    string? BillOfLadingCode,
    string? BillOfLadingName,
    int PageIndex,
    int PageSize)
    : IHttpRequest;