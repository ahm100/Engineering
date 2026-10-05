using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementExcelExporter;

public class GetEmployerStatusStatementExcelExporterQueryHandler : IQueryHandler<GetEmployerStatusStatementExcelExporterQuery, EmployerStatusStatement>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetEmployerStatusStatementExcelExporterQueryHandler> _logger;

    public GetEmployerStatusStatementExcelExporterQueryHandler(ILogger<GetEmployerStatusStatementExcelExporterQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(GetEmployerStatusStatementExcelExporterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetEmployerStatusStatementExcelExporter(request.Id, ct);

            return result ?? Result.Failure<EmployerStatusStatement>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}