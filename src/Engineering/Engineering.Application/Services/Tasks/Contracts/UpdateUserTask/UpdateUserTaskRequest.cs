using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Application.Services.Tasks.Contracts.UpdateUserTask;
public record UpdateUserTaskRequest(
    long Id,
    string Title,
    string? Description,
    long TaskGroupId,    
    TaskStatusEnum? Status
) : IHttpRequest;


