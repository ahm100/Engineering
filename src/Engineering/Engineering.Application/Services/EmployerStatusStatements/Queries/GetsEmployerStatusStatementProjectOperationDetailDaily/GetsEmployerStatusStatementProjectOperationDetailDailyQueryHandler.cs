using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetailDaily;

public class GetsEmployerStatusStatementProjectOperationDetailDailyQueryHandler : IQueryHandler<GetsEmployerStatusStatementProjectOperationDetailDailyQuery, DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>>
{
    private readonly IEmployerStatusStatementProjectOperationDetailDailyRepository _repository;
    private readonly ILogger<GetsEmployerStatusStatementProjectOperationDetailDailyQueryHandler> _logger;

    public GetsEmployerStatusStatementProjectOperationDetailDailyQueryHandler(ILogger<GetsEmployerStatusStatementProjectOperationDetailDailyQueryHandler> logger, IEmployerStatusStatementProjectOperationDetailDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementProjectOperationDetailDaily>>?>> Handle(GetsEmployerStatusStatementProjectOperationDetailDailyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsEmployerStatusStatementProjectOperationDetailDaily(
                request.EmployerStatusStatementId,
                request.EmployerStatusStatementProjectOperationId,
                request.EmployerStatusStatementProjectOperationDetailId,
                request.PageIndex,
                request.PageSize,
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