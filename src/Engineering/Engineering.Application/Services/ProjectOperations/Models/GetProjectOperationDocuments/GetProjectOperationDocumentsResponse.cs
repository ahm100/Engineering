
namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationDocuments;

public record GetProjectOperationDocumentsResponse(
    long Id,
    List<string>? Urls
    );
