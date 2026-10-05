namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.CreateProjectOperationWbs;

public record CreateProjectOperationWbsRequest(
    long ProjectWbsId,
    List<long> ProjectOperationIds,
    bool IsActive) : IHttpRequest;