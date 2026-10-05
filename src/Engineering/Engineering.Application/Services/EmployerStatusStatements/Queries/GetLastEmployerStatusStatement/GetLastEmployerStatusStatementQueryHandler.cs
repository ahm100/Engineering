using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using EmployerStatusStatement = Engineering.Domain.Entities.EmployerStatusStatements.EmployerStatusStatement;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetLastEmployerStatusStatement;

public class GetLastEmployerStatusStatementQueryHandler : IQueryHandler<GetLastEmployerStatusStatementQuery, EmployerStatusStatement>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetLastEmployerStatusStatementQueryHandler> _logger;

    public GetLastEmployerStatusStatementQueryHandler(ILogger<GetLastEmployerStatusStatementQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(GetLastEmployerStatusStatementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetLast(request.EmployerId, request.CostCenterId, request.ProjectId, request.ContractCode, ct);

            return result ?? Result.Failure<EmployerStatusStatement?>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}