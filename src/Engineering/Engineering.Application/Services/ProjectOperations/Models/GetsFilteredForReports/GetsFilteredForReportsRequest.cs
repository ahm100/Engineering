namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;

public record GetsFilteredForReportsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
