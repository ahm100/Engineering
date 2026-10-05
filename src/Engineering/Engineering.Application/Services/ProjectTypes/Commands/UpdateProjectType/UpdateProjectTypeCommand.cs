using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.UpdateProjectType;

public record UpdateProjectTypeCommand(
    long Id,
    string ProjectTypeName,
    string ProjectTypeCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<ProjectType>;