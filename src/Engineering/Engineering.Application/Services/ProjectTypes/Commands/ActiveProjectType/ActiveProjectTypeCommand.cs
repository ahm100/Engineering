using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.ActiveProjectType;

public record ActiveProjectTypeCommand(
    long Id
    ) : ICommand<ProjectType>;