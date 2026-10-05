using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.DisableProjectType;

public record DisableProjectTypeCommand(
    long Id
    ) : ICommand<ProjectType>;