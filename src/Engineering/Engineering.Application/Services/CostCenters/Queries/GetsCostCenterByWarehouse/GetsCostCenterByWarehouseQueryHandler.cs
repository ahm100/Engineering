using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByWarehouse;

public class GetsCostCenterByWarehouseQueryHandler : IQueryHandler<GetsCostCenterByWarehouseQuery, DataResult<List<CostCenter>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsCostCenterByWarehouseQueryHandler> _logger;

    public GetsCostCenterByWarehouseQueryHandler(ILogger<GetsCostCenterByWarehouseQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenter>>?>> Handle(GetsCostCenterByWarehouseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterByWarehouse(request.WarehouseId, request.FilterData, request.CompanyId, request.PageIndex, request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<CostCenter>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostCenter>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostCenter>>>(SharedErrors.UnknownError);
        }
    }
}