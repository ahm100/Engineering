using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationsByProjectOperationIds;

public class GetDailyProjectOperationsByProjectOperationIdsQueryHandler : IQueryHandler<GetDailyProjectOperationsByProjectOperationIdsQuery, List<DailyProjectOperation>>
{
    private readonly ILogger<GetDailyProjectOperationsByProjectOperationIdsQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDailyProjectOperationsByProjectOperationIdsQueryHandler(ILogger<GetDailyProjectOperationsByProjectOperationIdsQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<DailyProjectOperation>?>> Handle(GetDailyProjectOperationsByProjectOperationIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDailyProjectOperationsByProjectOperationIds(request.ProjectOperationIds, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<DailyProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}
