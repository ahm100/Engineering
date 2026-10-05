using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.InactiveProjectService;

public record InactiveProjectServiceCommand(
    long Id
    ) : ICommand<ProjectService>;