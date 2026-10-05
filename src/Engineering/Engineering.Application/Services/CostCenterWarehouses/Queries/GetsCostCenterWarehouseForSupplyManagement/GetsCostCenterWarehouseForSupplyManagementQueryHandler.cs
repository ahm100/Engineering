using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseForSupplyManagement;

public class GetsCostCenterWarehouseForSupplyManagementQueryHandler : IQueryHandler<GetsCostCenterWarehouseForSupplyManagementQuery, DataResult<List<CostCenterWarehouse>>>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetsCostCenterWarehouseForSupplyManagementQueryHandler> _logger;

    public GetsCostCenterWarehouseForSupplyManagementQueryHandler(ILogger<GetsCostCenterWarehouseForSupplyManagementQueryHandler> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterWarehouse>>?>> Handle(GetsCostCenterWarehouseForSupplyManagementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterWarehouseForSupplyManagement(ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenterWarehouse>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenterWarehouse>>>(CostCenterWarehouseErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenterWarehouse>>>(SharedErrors.UnknownError);
        }
    }
}
