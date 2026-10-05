namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationById;

public record GetProjectOperationByIdRequest(
    long Id
     ) : IHttpRequest;
