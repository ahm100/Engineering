using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationRealtionInfo;

public record GetProjectOperationRealtionInfoQuery(
    long Id
    ) : IQuery<ProjectOperation>;