namespace Engineering.Application.Services.Tasks.Contracts.DeleteTaskGroup;

public record DeleteTaskGroupRequest(
    long Id
) : IHttpRequest;
