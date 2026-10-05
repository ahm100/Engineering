namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProject;

public record GetsFilteredByProjectRequest(
    long ProjectId,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    List<long>? ContractorIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
