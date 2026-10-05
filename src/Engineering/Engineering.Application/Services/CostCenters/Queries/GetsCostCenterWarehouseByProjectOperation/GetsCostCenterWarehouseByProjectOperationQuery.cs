
namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterWarehouseByProjectOperation;

public record GetsCostCenterWarehouseByProjectOperationQuery(
    long ProjectOperationId
    ) : IQuery<List<long>?>;