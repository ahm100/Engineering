namespace Engineering.Application.Services.Tasks.Contracts.CreateTaskGroup;

public record CreateTaskGroupRequest(
    string Title,
    string? Description
) : IHttpRequest;
