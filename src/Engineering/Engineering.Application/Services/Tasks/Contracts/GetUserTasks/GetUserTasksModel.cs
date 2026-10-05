using Engineering.Domain.Entities.Tasks.Enums;


namespace Engineering.Application.Services.Tasks.Contracts.GetUserTasks;


public record GetUserTasksModel(
    long Id,
    string Title,
    string? Description,
    long TaskGroupId,
    string TaskGroupTitle,
    TaskStatusEnum Status,
    long? OwnerThirdPartyId
);