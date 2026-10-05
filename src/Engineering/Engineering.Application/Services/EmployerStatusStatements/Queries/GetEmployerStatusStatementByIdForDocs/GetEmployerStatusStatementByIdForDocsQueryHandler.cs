using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementByIdForDocs;

public class GetEmployerStatusStatementByIdForDocsForDocsQueryHandler : IQueryHandler<GetEmployerStatusStatementByIdForDocsQuery, EmployerStatusStatement>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetEmployerStatusStatementByIdForDocsForDocsQueryHandler> _logger;

    public GetEmployerStatusStatementByIdForDocsForDocsQueryHandler(
        ILogger<GetEmployerStatusStatementByIdForDocsForDocsQueryHandler> logger,
        IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(GetEmployerStatusStatementByIdForDocsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetEmployerStatusStatementByIdForDocs(request.Id, ct);

            return result ?? Result.Failure<EmployerStatusStatement>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}