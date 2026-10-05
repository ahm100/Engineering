namespace Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;

public record GetCriticalPORequest(
    long ProjectId,
    int PageIndex,
    int PageSize) : IHttpRequest;