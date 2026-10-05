using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Commands.ProjectStatusChanger;

public record ProjectStatusChangerCommand(
    long Id,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    ProjectStatus Status,
    string? StatusDescription
    ) : ICommand<Project>;
