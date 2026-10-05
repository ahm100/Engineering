using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationByIdsIncludeless;

public record GetsProjectOperationByIdsIncludelessQuery(
    List<long>? Ids,
    string? FilterData,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperation>>>;