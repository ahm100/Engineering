namespace Engineering.Application.Services.Tasks.Contracts.DeleteUserTask;


public record DeleteUserTaskRequest(
    long Id
) : IHttpRequest;


