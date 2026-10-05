using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Application.Services.Tasks.Contracts.GetUserTasks;

public record GetUserTasksRequest(
    long? OwnerUserId,// if not in logic will check by creatorId
    long? TaskGroupId,
    TaskStatusEnum? Status,
    int? PageIndex,
    int? PageSize
) : IHttpRequest;