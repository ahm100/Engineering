using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenterWarehouse = Engineering.Domain.Entities.CostCenters.CostCenterWarehouse;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetById;

public class GetCostCenterWarehouseByIdQueryHandler : IQueryHandler<GetCostCenterWarehouseByIdQuery, CostCenterWarehouse?>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetCostCenterWarehouseByIdQueryHandler> _logger;

    public GetCostCenterWarehouseByIdQueryHandler(ILogger<GetCostCenterWarehouseByIdQueryHandler> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(GetCostCenterWarehouseByIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindCostCenterWarehouse(request.Id, ct);

            return item ?? Result.Failure<CostCenterWarehouse?>(CostCenterWarehouseErrors.IdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterWarehouse?>(SharedErrors.UnknownError);
        }
    }
}