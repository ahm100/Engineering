using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementProjectOperation;

public class GetsEmployerStatusStatementProjectOperationQueryHandler : IQueryHandler<GetsEmployerStatusStatementProjectOperationQuery, DataResult<List<EmployerStatusStatementProjectOperation>>>
{
    private readonly IEmployerStatusStatementProjectOperationRepository _repository;
    private readonly ILogger<GetsEmployerStatusStatementProjectOperationQueryHandler> _logger;

    public GetsEmployerStatusStatementProjectOperationQueryHandler(ILogger<GetsEmployerStatusStatementProjectOperationQueryHandler> logger, IEmployerStatusStatementProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementProjectOperation>>?>> Handle(GetsEmployerStatusStatementProjectOperationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsEmployerStatusStatementProjectOperation(request.EmployerStatusStatementId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<EmployerStatusStatementProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<EmployerStatusStatementProjectOperation>>>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<EmployerStatusStatementProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}