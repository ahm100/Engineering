using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyOperationCreatedByProjectReport;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyOperationCreatedByProjectReport;

public class GetsDailyOperationCreatedByProjectReportQueryHandler : IQueryHandler<GetsDailyOperationCreatedByProjectReportQuery, GetsDailyOperationCreatedByProjectReportResponse?>
{
    private readonly ILogger<GetsDailyOperationCreatedByProjectReportQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetsDailyOperationCreatedByProjectReportQueryHandler(ILogger<GetsDailyOperationCreatedByProjectReportQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsDailyOperationCreatedByProjectReportResponse?>> Handle(GetsDailyOperationCreatedByProjectReportQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDailyOperationCreatedByProjectReport(ct);

            return result.Any() ?
                new GetsDailyOperationCreatedByProjectReportResponse
                (
                    result,
                    result.Count
                ) : Result.Failure<GetsDailyOperationCreatedByProjectReportResponse?>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsDailyOperationCreatedByProjectReportResponse>(SharedErrors.UnknownError)!;
        }
    }
}
