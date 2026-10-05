using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationService;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationService;

public class GetsDailyProjectOperationServiceQueryHandler : IQueryHandler<GetsDailyProjectOperationServiceQuery, DataResult<List<GetsDailyProjectOperationServiceModel>>>
{
    private readonly ILogger<GetsDailyProjectOperationServiceQueryHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public GetsDailyProjectOperationServiceQueryHandler(ILogger<GetsDailyProjectOperationServiceQueryHandler> logger,
                                                       IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsDailyProjectOperationServiceModel>>?>> Handle(GetsDailyProjectOperationServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDailyProjectOperationService(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.MeasurUnitIds,
                request.StartDate,
                request.EndDate,
                request.FromDate,
                request.ToDate,
                request.Status,
                request.FilterData,
                request.FilterServiceInfo,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDailyProjectOperationServiceModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDailyProjectOperationServiceModel>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDailyProjectOperationServiceModel>>>(SharedErrors.UnknownError);
        }
    }
}
