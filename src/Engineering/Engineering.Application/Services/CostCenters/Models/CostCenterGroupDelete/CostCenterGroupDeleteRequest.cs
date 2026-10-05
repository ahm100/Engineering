
namespace Engineering.Application.Services.CostCenters.Models.CostCenterGroupDelete;

public record CostCenterGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
