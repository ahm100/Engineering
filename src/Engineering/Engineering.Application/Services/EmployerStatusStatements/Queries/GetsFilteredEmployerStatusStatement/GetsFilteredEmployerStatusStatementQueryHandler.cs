using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using EmployerStatusStatement = Engineering.Domain.Entities.EmployerStatusStatements.EmployerStatusStatement;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsFilteredEmployerStatusStatement;

public class GetsFilteredEmployerStatusStatementQueryHandler : IQueryHandler<GetsFilteredEmployerStatusStatementQuery, DataResult<List<EmployerStatusStatement>>>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetsFilteredEmployerStatusStatementQueryHandler> _logger;

    public GetsFilteredEmployerStatusStatementQueryHandler(ILogger<GetsFilteredEmployerStatusStatementQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<EmployerStatusStatement>>?>> Handle(GetsFilteredEmployerStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredEmployerStatusStatement(request.EmployerId, request.CostCenterId, request.ProjectId, request.EmployerContractId,
                request.StatusStatementCode, request.ProjectOperationIds, request.StartDate, request.EndDate, request.Status, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<EmployerStatusStatement>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<EmployerStatusStatement>>>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<EmployerStatusStatement>>>(SharedErrors.UnknownError);
        }
    }
}