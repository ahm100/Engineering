using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperation;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDetailedDailyProjectOperation;

public class GetsDetailedDailyProjectOperationQueryHandler : IQueryHandler<GetsDetailedDailyProjectOperationQuery, DataResult<List<GetsDetailedDailyProjectOperationModel>>>
{
    private readonly ILogger<GetsDetailedDailyProjectOperationQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetsDetailedDailyProjectOperationQueryHandler(ILogger<GetsDetailedDailyProjectOperationQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsDetailedDailyProjectOperationModel>>?>> Handle(GetsDetailedDailyProjectOperationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDetailedDailyProjectOperation(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.CreatorIds,
                request.StartDate,
                request.EndDate,
                request.FromDate,
                request.ToDate,
                request.ProjectOperationDetailStatus,
                request.Status,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDetailedDailyProjectOperationModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDetailedDailyProjectOperationModel>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDetailedDailyProjectOperationModel>>>(SharedErrors.UnknownError);
        }
    }
}
