namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrBasePricedPOs;

public record GetFltrBasePricedPOsRequest(
    long CostCenterId,
    List<long>? ProjectIds,
    List<long>? ActionIds,
    List<long>? CategoryIds,
    List<long>? BranchIds,
    List<long>? SeasonIds,
    List<long>? OperationInfoIds,
    string? FilterData,
    int PageIndex,
    int PageSize
) : IHttpRequest;
