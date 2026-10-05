using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenters;

public class GetCostCentersQueryHandler : IQueryHandler<GetCostCentersQuery, DataResult<List<GetCostCentersModel>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetCostCentersQueryHandler> _logger;

    public GetCostCentersQueryHandler(ILogger<GetCostCentersQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetCostCentersModel>>?>> Handle(GetCostCentersQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCenters(request.Ids, request.FilterData, request.CostCenterName, request.CostCenterCode, request.CostCenterTypeId, request.InformedUserId,
                request.AuthorizedRoleId, request.AuthorizedUserId, request.WarehouseId, request.CityId, request.IsActive, request.OrderBy, request.CompanyId, request.PageIndex, request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetCostCentersModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetCostCentersModel>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetCostCentersModel>>>(SharedErrors.UnknownError);
        }
    }
}