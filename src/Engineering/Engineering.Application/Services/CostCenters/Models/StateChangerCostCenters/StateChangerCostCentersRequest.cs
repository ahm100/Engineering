
namespace Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;

public record StateChangerCostCentersRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
