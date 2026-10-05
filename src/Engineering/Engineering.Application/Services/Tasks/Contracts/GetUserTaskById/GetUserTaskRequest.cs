using Engineering.Domain.Entities.Tasks.Enums;

namespace Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;


public record GetUserTaskRequest(
    long Id
) : IHttpRequest;



