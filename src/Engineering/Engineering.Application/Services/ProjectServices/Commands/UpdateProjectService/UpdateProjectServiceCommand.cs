using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ServiceInfos;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectService;

public record UpdateProjectServiceCommand(
    ProjectService ProjectService,
    ServiceInfo ServiceInfo,
    long ContractorId,
    decimal Volume,
    decimal DoneVolume,
    bool IsActive,
    List<OperationInfoService>? OperationInfoServices
    ) : ICommand<ProjectService>;