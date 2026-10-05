using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.CreateProjectType;

public record CreateProjectTypeCommand(
    string ProjectTypeName,
    string ProjectTypeCode,
    bool IsActive,
    long? CompanyId
    ) : ICommand<ProjectType>;