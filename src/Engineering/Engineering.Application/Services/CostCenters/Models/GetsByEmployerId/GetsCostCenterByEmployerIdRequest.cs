namespace Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;

public record GetsCostCenterByEmployerIdRequest(
    string? FilterData,
    long EmployerId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
