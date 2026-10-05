namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;

public record GetsFilteredBillOfLadingRequest(
    string? FilterData,
    string? BillOfLadingCode,
    string? BillOfLadingName,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;