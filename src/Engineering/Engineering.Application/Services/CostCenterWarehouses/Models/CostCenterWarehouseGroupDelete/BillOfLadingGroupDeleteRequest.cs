
namespace Engineering.Application.Services.CostCenterWarehouses.Models.CostCenterWarehouseGroupDelete;

public record CostCenterWarehouseGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
