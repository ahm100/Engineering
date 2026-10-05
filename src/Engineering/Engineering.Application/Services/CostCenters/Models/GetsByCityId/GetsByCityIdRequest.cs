namespace Engineering.Application.Services.CostCenters.Models.GetsByCityId;

public record GetsByCityIdRequest(
    long CityId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
