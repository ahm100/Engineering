using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.InactiveProject;

public record InactiveProjectCommand(
    long Id
    ) : ICommand<Project>;