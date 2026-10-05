namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByProjectIds;

public record GetsProjectOperationByProjectIdsRequest(
    List<long>? ProjectIds,
    List<long>? ContractorIds,
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
