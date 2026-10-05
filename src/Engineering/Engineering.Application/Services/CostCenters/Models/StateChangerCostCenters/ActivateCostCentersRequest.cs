
namespace Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;

public record ActivateCostCentersRequest(
    List<long> Ids
    ) : IHttpRequest;
