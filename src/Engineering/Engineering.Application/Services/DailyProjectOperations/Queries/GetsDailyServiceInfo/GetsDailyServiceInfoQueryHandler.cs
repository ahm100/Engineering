using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyServiceInfo;

public class GetsDailyServiceInfoQueryHandler : IQueryHandler<GetsDailyServiceInfoQuery, DataResult<List<GetsDailyServiceInfoModel>>>
{
    private readonly ILogger<GetsDailyServiceInfoQueryHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public GetsDailyServiceInfoQueryHandler(ILogger<GetsDailyServiceInfoQueryHandler> logger,
                                                       IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsDailyServiceInfoModel>>?>> Handle(GetsDailyServiceInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDailyServiceInfo(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDailyServiceInfoModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDailyServiceInfoModel>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDailyServiceInfoModel>>>(SharedErrors.UnknownError);
        }
    }
}
