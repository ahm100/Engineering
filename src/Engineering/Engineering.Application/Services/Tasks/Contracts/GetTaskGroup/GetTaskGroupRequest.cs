namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;

public record GetTaskGroupRequest(
    long Id
) : IHttpRequest;
