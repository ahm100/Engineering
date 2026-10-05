
namespace Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;

public record InactivateCostCentersRequest(
    List<long> Ids
    ) : IHttpRequest;
