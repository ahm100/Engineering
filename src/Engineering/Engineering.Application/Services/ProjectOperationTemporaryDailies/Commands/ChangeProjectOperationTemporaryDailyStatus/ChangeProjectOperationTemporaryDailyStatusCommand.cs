using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.ChangeProjectOperationTemporaryDailyStatus;

public record ChangeProjectOperationTemporaryDailyStatusCommand(
    ProjectOperationTemporaryDaily Entity,
    TemporaryDailyStatus Status
    ) : ICommand<ProjectOperationTemporaryDaily>;
