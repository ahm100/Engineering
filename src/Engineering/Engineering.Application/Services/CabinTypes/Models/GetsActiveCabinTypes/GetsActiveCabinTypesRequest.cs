namespace Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;

public record GetsActiveCabinTypesRequest(
    string? FilterData,
    int? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;