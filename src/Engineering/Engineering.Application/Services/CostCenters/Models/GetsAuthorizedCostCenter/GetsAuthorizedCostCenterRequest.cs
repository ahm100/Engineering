namespace Engineering.Application.Services.CostCenters.Models.GetsAuthorizedCostCenter;

public record GetsAuthorizedCostCenterRequest(
    string? FilterData,
    long? EmployerId,
    long? CostCenterTypeId,
    long? CityId,
    string? Name,
    string? Code,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
