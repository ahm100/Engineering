namespace Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;

public record CreateProjectTypeRequest(
    string ProjectTypeCode,
    string ProjectTypeName,
    bool IsActive
     ) : IHttpRequest;
