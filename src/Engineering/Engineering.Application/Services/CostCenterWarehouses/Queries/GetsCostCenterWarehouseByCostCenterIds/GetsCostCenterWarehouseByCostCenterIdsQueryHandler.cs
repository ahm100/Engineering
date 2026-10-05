using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseByCostCenterIds;

public class GetsCostCenterWarehouseByCostCenterIdsQueryHandler : IQueryHandler<GetsCostCenterWarehouseByCostCenterIdsQuery, DataResult<List<CostCenterWarehouse>>>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetsCostCenterWarehouseByCostCenterIdsQueryHandler> _logger;

    public GetsCostCenterWarehouseByCostCenterIdsQueryHandler(ILogger<GetsCostCenterWarehouseByCostCenterIdsQueryHandler> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenterWarehouse>>?>> Handle(GetsCostCenterWarehouseByCostCenterIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterWarehouseByCostCenterIds(request.CostCenterIds, request.PageIndex, request.PageSize, ct);

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