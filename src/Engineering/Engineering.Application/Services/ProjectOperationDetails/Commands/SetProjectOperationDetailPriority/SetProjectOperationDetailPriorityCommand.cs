using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.SetProjectOperationDetailPriority;

public record SetProjectOperationDetailPriorityCommand(
    long Id,
    int Priority
    ) : ICommand<ProjectOperationDetail>;
