using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.ProjectOperationWorkloadManagement;

public record ProjectOperationWorkloadManagementQuery(
    long Id
    ) : IQuery<ProjectOperation>;