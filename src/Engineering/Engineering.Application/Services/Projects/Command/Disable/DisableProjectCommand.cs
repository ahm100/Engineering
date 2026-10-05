using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.Disable;

public record DisableProjectCommand(
    long Id
    ) : ICommand<Project>;
