using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.DisableProjectService;

public record DeleteProjectServiceCommand(
    long Id
    ) : ICommand<ProjectService>;