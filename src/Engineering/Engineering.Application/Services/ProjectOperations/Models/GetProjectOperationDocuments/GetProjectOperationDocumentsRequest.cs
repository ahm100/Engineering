namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationDocuments;

public record GetProjectOperationDocumentsRequest(
    long Id
     ) : IHttpRequest;
