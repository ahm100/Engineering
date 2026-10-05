using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.DeleteImplementationAssistans;

public record DeleteImplementationAssistansCommand(
    long ImplementationAssistansId,
    long ProjectId
    ) : ICommand<ProjectImplementationAssistant>;