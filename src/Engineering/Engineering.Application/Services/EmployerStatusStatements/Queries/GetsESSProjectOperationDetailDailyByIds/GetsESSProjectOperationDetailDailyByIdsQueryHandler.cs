using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationDetailDailyByIds;

public class GetsESSProjectOperationDetailDailyByIdsQueryHandler : IQueryHandler<GetsESSProjectOperationDetailDailyByIdsQuery, DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>
{
    private readonly IEmployerStatusStatementProjectOperationDetailDailyRepository _repository;
    private readonly ILogger<GetsESSProjectOperationDetailDailyByIdsQueryHandler> _logger;

    public GetsESSProjectOperationDetailDailyByIdsQueryHandler(ILogger<GetsESSProjectOperationDetailDailyByIdsQueryHandler> logger, IEmployerStatusStatementProjectOperationDetailDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>?>> Handle(GetsESSProjectOperationDetailDailyByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsESSProjectOperationDetailDailyByIds(
                request.Ids,
                ct);

            return result.Data.Any() ?
                new DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>(SharedErrors.UnknownError);
        }
    }
}