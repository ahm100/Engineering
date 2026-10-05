using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsWithContractProjectOperationForESS;

public record GetsWithContractProjectOperationForESSQuery(
    long ProjectId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;
