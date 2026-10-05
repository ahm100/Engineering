using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetEmployerStatusStatementLimitDate;

public class GetEmployerStatusStatementLimitDateQueryHandler : IQueryHandler<GetEmployerStatusStatementLimitDateQuery, List<DailyProjectOperation>>
{
    private readonly ILogger<GetEmployerStatusStatementLimitDateQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetEmployerStatusStatementLimitDateQueryHandler(ILogger<GetEmployerStatusStatementLimitDateQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<DailyProjectOperation>?>> Handle(GetEmployerStatusStatementLimitDateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetEmployerStatusStatementLimitDate(request.ProjectId, request.EmployerContractId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<DailyProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}
