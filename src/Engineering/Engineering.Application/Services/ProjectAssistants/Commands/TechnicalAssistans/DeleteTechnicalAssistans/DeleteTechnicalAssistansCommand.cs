using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.DeleteTechnicalAssistans;

public record DeleteTechnicalAssistansCommand(
    long TechnicalAssistantId,
    long ProjectId
    ) : ICommand<ProjectTechnicalAssistant>;