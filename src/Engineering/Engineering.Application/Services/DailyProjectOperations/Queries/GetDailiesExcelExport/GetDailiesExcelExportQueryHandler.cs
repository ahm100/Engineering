using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailiesExcelExport;

public class GetDailiesExcelExportQueryHandler : IQueryHandler<GetDailiesExcelExportQuery, DataResult<List<DailyProjectOperation>>>
{
    private readonly ILogger<GetDailiesExcelExportQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDailiesExcelExportQueryHandler(ILogger<GetDailiesExcelExportQueryHandler> logger, IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<DailyProjectOperation>>?>> Handle(GetDailiesExcelExportQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredDailyProjectOperationForExcel(request.Ids, request.ProjectOperationDetailId, request.StartDate,
                request.EndDate, request.CreatorId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<DailyProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<DailyProjectOperation>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<DailyProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}
