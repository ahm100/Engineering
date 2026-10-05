using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsActiveMainWarehouseCostCenter;

public class GetsActiveMainWarehouseCostCenterQueryHandler : IQueryHandler<GetsActiveMainWarehouseCostCenterQuery, DataResult<List<GetsActiveMainWarehouseCostCenterModel>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsActiveMainWarehouseCostCenterQuery> _logger;

    public GetsActiveMainWarehouseCostCenterQueryHandler(
        ILogger<GetsActiveMainWarehouseCostCenterQuery> logger,
        ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveMainWarehouseCostCenterModel>>?>> Handle(GetsActiveMainWarehouseCostCenterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveMainWarehouseCostCenter(
                request.FilterData,
                request.WarehouseIds,
                request.CompanyId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveMainWarehouseCostCenterModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveMainWarehouseCostCenterModel>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveMainWarehouseCostCenterModel>>>(SharedErrors.UnknownError);
        }
    }
}