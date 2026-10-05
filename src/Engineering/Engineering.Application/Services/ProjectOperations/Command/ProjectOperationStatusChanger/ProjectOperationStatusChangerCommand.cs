using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Commands.ProjectOperationStatusChanger;

public record ProjectOperationStatusChangerCommand(
    long Id,
    ProjectOperationStatus Status
    ) : ICommand<ProjectOperation>;
