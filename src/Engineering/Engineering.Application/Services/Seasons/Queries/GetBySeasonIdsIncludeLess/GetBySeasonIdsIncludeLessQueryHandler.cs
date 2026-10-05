using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetBySeasonIdsIncludeLess;

public class GetBySeasonIdsIncludeLessQueryHandler : IQueryHandler<GetBySeasonIdsIncludeLessQuery, DataResult<List<Season>>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetBySeasonIdsIncludeLessQueryHandler> _logger;

    public GetBySeasonIdsIncludeLessQueryHandler(ILogger<GetBySeasonIdsIncludeLessQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Season>>?>> Handle(GetBySeasonIdsIncludeLessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetBySeasonIdsIncludeLess(request.SeasonIds, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Season>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Season>>>(SeasonErrors.SeasonWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Season>>>(SharedErrors.UnknownError);
        }
    }
}