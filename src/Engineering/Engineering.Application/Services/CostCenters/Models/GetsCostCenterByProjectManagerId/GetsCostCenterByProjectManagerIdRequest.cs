namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;

public record GetsCostCenterByProjectManagerIdRequest(
    string? FilterData,
    long ProjectManagerId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
