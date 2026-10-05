using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsAuthorizedCostCenter;

public class GetsAuthorizedCostCenterQueryHandler : IQueryHandler<GetsAuthorizedCostCenterQuery, DataResult<List<CostCenter>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsAuthorizedCostCenterQuery> _logger;

    public GetsAuthorizedCostCenterQueryHandler(
        ILogger<GetsAuthorizedCostCenterQuery> logger,
        ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenter>>?>> Handle(GetsAuthorizedCostCenterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsAuthorizedCostCenter(
                request.FilterData,
                request.UserId,
                request.EmployerId,
                request.CostCenterTypeId,
                request.CityId,
                request.Code,
                request.Name,
                request.CompanyId,
                request.PageIndex,
                request.PageSize,
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
