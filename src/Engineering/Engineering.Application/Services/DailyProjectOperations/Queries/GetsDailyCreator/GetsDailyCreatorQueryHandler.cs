using Engineering.Application.Abstractions.Data.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyCreator;

public class GetsDailyCreatorQueryHandler : IQueryHandler<GetsDailyCreatorQuery, DataResult<List<long>>>
{
    private readonly IDailyProjectOperationRepository _repository;
    private readonly ILogger<GetsDailyCreatorQueryHandler> _logger;

    public GetsDailyCreatorQueryHandler(ILogger<GetsDailyCreatorQueryHandler> logger, IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsDailyCreatorQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsDailyCreator(ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
