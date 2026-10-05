using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdIncludeLess;

public record GetsProjectOperationByIdIncludeLessQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ProjectOperation>>>;