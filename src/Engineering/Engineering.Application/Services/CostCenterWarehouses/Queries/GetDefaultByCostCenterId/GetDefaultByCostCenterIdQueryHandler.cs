using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterId;

public class GetDefaultByCostCenterIdQueryHandler : IQueryHandler<GetDefaultByCostCenterIdQuery, CostCenterWarehouse>
{
    private readonly ICostCenterWarehouseRepository _repository;
    private readonly ILogger<GetDefaultByCostCenterIdQueryHandler> _logger;

    public GetDefaultByCostCenterIdQueryHandler(ILogger<GetDefaultByCostCenterIdQueryHandler> logger, ICostCenterWarehouseRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostCenterWarehouse?>> Handle(GetDefaultByCostCenterIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetDefaultByCostCenterId(request.costCenterId, ct);

            return item ?? Result.Failure<CostCenterWarehouse?>(CostCenterWarehouseErrors.IdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenterWarehouse?>(SharedErrors.UnknownError);
        }
    }
}
