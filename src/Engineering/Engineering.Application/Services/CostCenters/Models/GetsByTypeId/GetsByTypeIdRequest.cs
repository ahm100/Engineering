namespace Engineering.Application.Services.CostCenters.Models.GetsByTypeId;

public record GetsByTypeIdRequest(
    long CostCenterTypeId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
