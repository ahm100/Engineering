namespace Engineering.Application.Services.CabinTypes.Models.GetsCabinType;

public record GetsCabinTypeRequest(
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize)
    : IHttpRequest;