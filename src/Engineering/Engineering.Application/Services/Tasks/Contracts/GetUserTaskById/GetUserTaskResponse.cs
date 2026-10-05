
using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;

public record GetUserTaskResponse
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public long TaskGroupId { get; init; }
    public string TaskGroupTitle { get; init; }
    public TaskStatusEnum Status { get; init; }
    public long? OwnerUserId { get; init; }
    public long? CreatorId { get; init; }
};