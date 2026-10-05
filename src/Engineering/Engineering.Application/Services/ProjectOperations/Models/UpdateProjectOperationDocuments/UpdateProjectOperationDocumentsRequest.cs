
namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationDocuments;

public record UpdateProjectOperationDocumentsRequest(
    long Id,
    List<string>? Urls
     ) : IHttpRequest;
