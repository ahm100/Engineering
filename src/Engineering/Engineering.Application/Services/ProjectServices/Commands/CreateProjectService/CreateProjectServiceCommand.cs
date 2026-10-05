using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.ServiceInfos;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.CreateProjectService;

public record CreateProjectServiceCommand(
    Project Project,
    ServiceInfo ServiceInfo,
    long ContractorId,
    decimal Volume,
    decimal DoneVolume,
    bool IsActive,
    List<OperationInfoService>? OperationInfoServices
    ) : ICommand<ProjectService>;