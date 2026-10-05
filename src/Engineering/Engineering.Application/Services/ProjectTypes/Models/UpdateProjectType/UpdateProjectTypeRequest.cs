namespace Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;

public record UpdateProjectTypeRequest(
    long Id,
    string ProjectTypeName,
    string ProjectTypeCode,
    bool IsActive
     ) : IHttpRequest;
