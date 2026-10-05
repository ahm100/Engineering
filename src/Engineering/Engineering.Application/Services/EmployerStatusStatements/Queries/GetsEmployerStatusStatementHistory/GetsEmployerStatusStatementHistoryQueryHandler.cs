using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementHistory;

public class GetsEmployerStatusStatementHistoryQueryHandler : IQueryHandler<GetsEmployerStatusStatementHistoryQuery, DataResult<List<EmployerStatusStatementHistory>>>
{
    private readonly IEmployerStatusStatementHistoryRepository _repository;
    private readonly ILogger<GetsEmployerStatusStatementHistoryQueryHandler> _logger;

    public GetsEmployerStatusStatementHistoryQueryHandler(ILogger<GetsEmployerStatusStatementHistoryQueryHandler> logger, IEmployerStatusStatementHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementHistory>>?>> Handle(GetsEmployerStatusStatementHistoryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsEmployerStatusStatementHistory(request.EmployerStatusStatementId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<EmployerStatusStatementHistory>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<EmployerStatusStatementHistory>>>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<EmployerStatusStatementHistory>>>(SharedErrors.UnknownError);
        }
    }
}