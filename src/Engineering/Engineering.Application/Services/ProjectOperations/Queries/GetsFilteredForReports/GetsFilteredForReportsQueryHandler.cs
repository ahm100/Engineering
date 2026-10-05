using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredForReports;

public class GetsFilteredForReportsQueryHandler : IQueryHandler<GetsFilteredForReportsQuery, DataResult<List<GetsFilteredForReportsModel>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetsFilteredForReportsQueryHandler> _logger;

    public GetsFilteredForReportsQueryHandler(ILogger<GetsFilteredForReportsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsFilteredForReportsModel>>?>> Handle(GetsFilteredForReportsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredForReports(request.CostCenterIds, request.ProjectIds,
                request.FilterData, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsFilteredForReportsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsFilteredForReportsModel>>>(ProjectOperationErrors.OperationInfoNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsFilteredForReportsModel>>>(SharedErrors.UnknownError);
        }
    }
}