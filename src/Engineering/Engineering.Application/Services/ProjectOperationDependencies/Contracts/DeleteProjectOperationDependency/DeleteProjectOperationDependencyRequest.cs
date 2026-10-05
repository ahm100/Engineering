namespace Engineering.Application.Services.ProjectOperationDependencies.Contracts.DeleteProjectOperationDependency;

public record DeleteProjectOperationDependencyRequest(
    long Id
     ) : IHttpRequest;
