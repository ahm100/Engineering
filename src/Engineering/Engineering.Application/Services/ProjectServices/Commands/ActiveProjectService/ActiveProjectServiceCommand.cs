using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.ActiveProjectService;

public record ActiveProjectServiceCommand(
    long Id
    ) : ICommand<ProjectService>;