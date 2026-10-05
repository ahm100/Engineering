using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationDocuments;

public record GetProjectOperationDocumentsQuery(
    long Id
    ) : IQuery<ProjectOperation>;
