using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementById;

public class GetEmployerStatusStatementByIdForDocsQueryHandler : IQueryHandler<GetEmployerStatusStatementByIdQuery, EmployerStatusStatement>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetEmployerStatusStatementByIdForDocsQueryHandler> _logger;

    public GetEmployerStatusStatementByIdForDocsQueryHandler(ILogger<GetEmployerStatusStatementByIdForDocsQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(GetEmployerStatusStatementByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetEmployerStatusStatementById(request.Id, ct);

            return result ?? Result.Failure<EmployerStatusStatement>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}