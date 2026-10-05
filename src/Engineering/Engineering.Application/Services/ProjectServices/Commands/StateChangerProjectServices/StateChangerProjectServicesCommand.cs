using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.StateChangerProjectServices;

public record StateChangerProjectServicesCommand(
    List<ProjectService> Items,
    bool State
    ) : ICommand<bool?>;
