namespace Engineering.Application.Services.ProjectTypes.Models.GetActiveProjectTypes;

public record GetActiveProjectTypesRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;