using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Application.Services.Tasks.Contracts.CreateUserTask;

public record CreateUserTaskRequest(
    string Title,
    string? Description,
    long TaskGroupId,
    long? OwnerId
) : IHttpRequest;


