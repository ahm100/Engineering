using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.SetManagerToProjects;

public record SetManagerToProjectsCommand(
    List<Project> Projects,
    long ProjectManager
    ) : ICommand<bool>;