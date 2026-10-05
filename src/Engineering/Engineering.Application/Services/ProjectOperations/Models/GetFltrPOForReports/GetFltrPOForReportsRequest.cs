namespace Engineering.Application.Services.ProjectOperations.Models.GetFltrPOForReports;

public record GetFltrPOForReportsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
