namespace Engineering.Application.Services.ProjectOperations.Models.DeleteProjectOperation;

public record DeleteProjectOperationRequest(
    long Id
     ) : IHttpRequest;
