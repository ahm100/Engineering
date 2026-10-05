using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetCostCenterWarehousesByWarehouseId;

public class GetCostCenterWarehousesByWarehouseIdQueryHandler : IQueryHandler<GetCostCenterWarehousesByWarehouseIdQuery, List<CostCenterWarehouse>>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetCostCenterWarehousesByWarehouseIdQueryHandler> _logger;

    public GetCostCenterWarehousesByWarehouseIdQueryHandler(ILogger<GetCostCenterWarehousesByWarehouseIdQueryHandler> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CostCenterWarehouse>?>> Handle(GetCostCenterWarehousesByWarehouseIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByWarehouseIds(request.WarehouseIds, ct);

            return result.Any() ? result : Result.Failure<List<CostCenterWarehouse>>(CostCenterWarehouseErrors.DataIsNull);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CostCenterWarehouse>>(SharedErrors.UnknownError);
        }
    }
}
