using Engineering.Application.Abstractions.Data.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByProjectManagerId;

public class GetsCostCenterByProjectManagerIdQueryHandler : IQueryHandler<GetsCostCenterByProjectManagerIdQuery, DataResult<List<CostCenter>>>
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<GetsCostCenterByProjectManagerIdQueryHandler> _logger;

    public GetsCostCenterByProjectManagerIdQueryHandler(ILogger<GetsCostCenterByProjectManagerIdQueryHandler> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostCenter>>?>> Handle(GetsCostCenterByProjectManagerIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostCenterByProjectManagerId(request.FilterData, request.ProjectManagerId, request.companyId, request.PageIndex, request.PageSize, ct);

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