namespace Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;

public record GetsCostCenterTypeRequest(
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;