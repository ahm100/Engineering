using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsByProject;

public record GetsByProjectQuery(
    long ProjectId,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsProjectOperationByProjectModel>>>;