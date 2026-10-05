using Engineering.Domain.Entities.Projects;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.CreateImplementationAssistans;

public record CreateImplementationAssistansCommand(
    Project Project,
    long ImplementationAssistantUserId
    ) : ICommand<ProjectImplementationAssistant>;