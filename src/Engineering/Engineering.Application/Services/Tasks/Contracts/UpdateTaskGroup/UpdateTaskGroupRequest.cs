namespace Engineering.Application.Services.Tasks.Contracts.UpdateTaskGroup;

public record UpdateTaskGroupRequest(
    long Id,
    string Title,
    string? Description
) : IHttpRequest;



public record UpdateTaskGroupResponse(
    bool Success
);
