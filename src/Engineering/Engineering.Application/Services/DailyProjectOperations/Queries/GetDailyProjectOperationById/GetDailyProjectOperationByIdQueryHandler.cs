using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationById;

public class GetDailyProjectOperationByIdQueryHandler : IQueryHandler<GetDailyProjectOperationByIdQuery, DailyProjectOperation>
{
    private readonly ILogger<GetDailyProjectOperationByIdQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDailyProjectOperationByIdQueryHandler(ILogger<GetDailyProjectOperationByIdQueryHandler> logger,
                                                    IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DailyProjectOperation?>> Handle(GetDailyProjectOperationByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdAsync(request.DailyProjectOperationId, ct);

            return result ?? Result.Failure<DailyProjectOperation>(DailyProjectOperationErrors.DailyProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DailyProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
