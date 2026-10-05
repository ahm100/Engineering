namespace Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;

public record UpdateProjectTypeResponse(
    long Id,
    string ProjectTypeName,
    string ProjectTypeCode
    );
