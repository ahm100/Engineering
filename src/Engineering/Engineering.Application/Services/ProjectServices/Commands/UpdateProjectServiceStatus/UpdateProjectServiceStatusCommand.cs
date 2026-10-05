using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceStatus;

public record UpdateProjectServiceStatusCommand(
    ProjectService ProjectService,
    ContractorServiceStatus Status
    ) : ICommand<ProjectService>;