using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsESSProjectOperationByIds;

public class GetsESSProjectOperationByIdsQueryHandler : IQueryHandler<GetsESSProjectOperationByIdsQuery, DataResult<List<EmployerStatusStatementProjectOperation>>>
{
    private readonly IEmployerStatusStatementProjectOperationRepository _repository;
    private readonly ILogger<GetsESSProjectOperationByIdsQueryHandler> _logger;

    public GetsESSProjectOperationByIdsQueryHandler(ILogger<GetsESSProjectOperationByIdsQueryHandler> logger, IEmployerStatusStatementProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatementProjectOperation>>?>> Handle(GetsESSProjectOperationByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsESSProjectOperationByIds(request.Ids, ct);

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