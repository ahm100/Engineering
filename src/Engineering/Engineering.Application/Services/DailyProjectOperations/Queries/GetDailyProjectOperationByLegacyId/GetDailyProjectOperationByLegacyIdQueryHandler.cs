using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationByLegacyId;

public class GetDailyProjectOperationByLegacyIdQueryHandler : IQueryHandler<GetDailyProjectOperationByLegacyIdQuery, DailyProjectOperation>
{
    private readonly ILogger<GetDailyProjectOperationByLegacyIdQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDailyProjectOperationByLegacyIdQueryHandler(ILogger<GetDailyProjectOperationByLegacyIdQueryHandler> logger,
                                                    IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperation?>> Handle(GetDailyProjectOperationByLegacyIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDailyProjectOperationByLegacyId(request.LegacyId, ct);

            return result ?? Result.Failure<DailyProjectOperation>(DailyProjectOperationErrors.DailyProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
