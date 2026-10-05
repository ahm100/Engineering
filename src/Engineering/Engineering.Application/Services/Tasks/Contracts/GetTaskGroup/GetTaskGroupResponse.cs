namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;

public record GetTaskGroupResponse
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public long CreatorId { get; init; }
};