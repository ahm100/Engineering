namespace Engineering.Application.Services.CostCenters.Models.GetsByNameOrCode;

public record GetsCostCenterByNameOrCodeRequest(
    string FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
