using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.InactiveProjectType;

public record InactiveProjectTypeCommand(
    long Id
    ) : ICommand<ProjectType>;