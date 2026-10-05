namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;

public record GetProjectOperationProgressRequest(
    long Id
     ) : IHttpRequest;
