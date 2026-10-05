using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailStatusChanger;

public record ProjectOperationDetailStatusChangerCommand(
    long Id,
    ProjectOperationDetailStatus Status,
    string? StatusDescription
    ) : ICommand<ProjectOperationDetail>;
