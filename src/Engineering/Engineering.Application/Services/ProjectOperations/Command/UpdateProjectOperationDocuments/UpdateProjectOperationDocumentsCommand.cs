using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationDocuments;

public record UpdateProjectOperationDocumentsCommand(
    ProjectOperation ProjectOperation,
    List<string>? Urls
    ) : ICommand<ProjectOperation>;
