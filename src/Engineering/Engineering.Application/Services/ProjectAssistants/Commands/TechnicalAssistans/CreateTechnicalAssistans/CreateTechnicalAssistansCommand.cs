using Engineering.Domain.Entities.Projects;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.CreateTechnicalAssistans;

public record CreateTechnicalAssistansCommand(
    Project Project,
    long TechnicalAssistantUserId
    ) : ICommand<ProjectTechnicalAssistant>;