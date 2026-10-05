using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceDoneVolume;

public record UpdateProjectServiceDoneVolumeCommand(
    long Id
    ) : ICommand<ProjectService>;