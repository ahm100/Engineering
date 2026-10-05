using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetOperationTotals;
using DailyProjectOperation = Engineering.Domain.Entities.DailyProjectOperations.DailyProjectOperation;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetOperationTotals;

public class GetOperationTotalsQueryHandler : IQueryHandler<GetOperationTotalsQuery, List<DailyProjectOperation>>
{
    private readonly IDailyProjectOperationRepository _repository;
    private readonly ILogger<GetOperationTotalsQueryHandler> _logger;

    public GetOperationTotalsQueryHandler(ILogger<GetOperationTotalsQueryHandler> logger, IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<DailyProjectOperation>?>> Handle(GetOperationTotalsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationTotals(request.ProjectOperationId, request.ProjectOperationDetailId, ct);

            return result.Any() ? result : Result.Failure<List<DailyProjectOperation>>(ProjectOperationErrors.ProjectOperationChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<DailyProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}