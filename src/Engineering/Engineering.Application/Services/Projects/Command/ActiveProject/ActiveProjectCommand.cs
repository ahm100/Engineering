using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.ActiveProject;

public record ActiveProjectCommand(
    long Id
    ) : ICommand<Project>;