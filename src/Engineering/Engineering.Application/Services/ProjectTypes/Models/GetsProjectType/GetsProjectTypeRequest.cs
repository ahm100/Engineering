namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectType;

public record GetsProjectTypeRequest(
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
