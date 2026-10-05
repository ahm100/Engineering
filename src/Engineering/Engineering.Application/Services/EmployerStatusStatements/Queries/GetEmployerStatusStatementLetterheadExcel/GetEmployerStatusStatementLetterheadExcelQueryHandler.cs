using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementLetterheadExcel;

public class GetEmployerStatusStatementLetterheadExcelQueryHandler : IQueryHandler<GetEmployerStatusStatementLetterheadExcelQuery, GetEmployerStatusStatementLetterheadExcelModel>
{
    private readonly IEmployerStatusStatementRepository _repository;
    private readonly ILogger<GetEmployerStatusStatementLetterheadExcelQueryHandler> _logger;

    public GetEmployerStatusStatementLetterheadExcelQueryHandler(ILogger<GetEmployerStatusStatementLetterheadExcelQueryHandler> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetEmployerStatusStatementLetterheadExcelModel?>> Handle(GetEmployerStatusStatementLetterheadExcelQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetEmployerStatusStatementLetterheadExcel(request.Id, ct);

            return result ?? Result.Failure<GetEmployerStatusStatementLetterheadExcelModel>(EmployerStatusStatementErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetEmployerStatusStatementLetterheadExcelModel>(SharedErrors.UnknownError);
        }
    }
}