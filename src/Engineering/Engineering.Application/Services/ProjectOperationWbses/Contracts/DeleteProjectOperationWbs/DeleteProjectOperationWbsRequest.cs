namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;

public record DeleteProjectOperationWbsRequest(
    long Id) : IHttpRequest;