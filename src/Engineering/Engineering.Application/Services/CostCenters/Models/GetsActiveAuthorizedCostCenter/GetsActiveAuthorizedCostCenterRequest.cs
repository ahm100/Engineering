namespace Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;

public record GetsActiveAuthorizedCostCenterRequest(
    string? FilterData,
    long? EmployerId,
    long? CostCenterTypeId,
    long? CityId,
    string? Name,
    string? Code,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
