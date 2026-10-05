namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;

public record GetProjectOperationWbsByIdRequest(
    long Id) : IHttpRequest;