using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.StateChangerProjects;

public record StateChangerProjectsCommand(
    List<Project> Items,
    bool State
    ) : ICommand<bool?>;
