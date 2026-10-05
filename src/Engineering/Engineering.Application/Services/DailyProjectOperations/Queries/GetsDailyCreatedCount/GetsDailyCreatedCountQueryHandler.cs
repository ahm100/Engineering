using Engineering.Application.Abstractions.Data.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyCreatedCount;

public class GetsDailyCreatedCountQueryHandler : IQueryHandler<GetsDailyCreatedCountQuery, int?>
{
    private readonly ILogger<GetsDailyCreatedCountQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetsDailyCreatedCountQueryHandler(ILogger<GetsDailyCreatedCountQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<int?>> Handle(GetsDailyCreatedCountQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDailyCreatedCount(ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<int?>(SharedErrors.UnknownError)!;
        }
    }
}
