using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperationDetail;

public class GetsEmployerStatusStatementProjectOperationDetailQueryHandler : IQueryHandler<GetsEmployerStatusStatementProjectOperationDetailQuery, DataResult<List<EmployerStatusStatementProjectOperationDetail>>>
{
    private readonly IEmployerStatusStatementProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsEmployerStatusStatementProjectOperationDetailQueryHandler> _logger;

    public GetsEmployerStatusStatementProjectOperationDetailQueryHandler(ILogger<GetsEmployerStatusStatementProjectOperationDetailQueryHandler> logger, IEmployerStatusStatementProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementProjectOperationDetail>>?>> Handle(GetsEmployerStatusStatementProjectOperationDetailQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsEmployerStatusStatementProjectOperationDetail(
                request.EmployerStatusStatementId,
                request.EmployerStatusStatementProjectOperationId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<EmployerStatusStatementProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<EmployerStatusStatementProjectOperationDetail>>>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<EmployerStatusStatementProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}