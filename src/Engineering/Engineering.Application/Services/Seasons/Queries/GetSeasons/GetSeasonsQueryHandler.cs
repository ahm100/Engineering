using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasons;

public class GetSeasonsQueryHandler : IQueryHandler<GetSeasonsQuery, DataResult<List<Season>>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetSeasonsQueryHandler> _logger;

    public GetSeasonsQueryHandler(ILogger<GetSeasonsQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Season>>?>> Handle(GetSeasonsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetSeasons(request.Ids, request.FilterData, request.BranchId, request.CategoryId, request.SeasonCode, request.SeasonName, request.IsActive, request.OrderBy, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Season>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Season>>>(SeasonErrors.FilteredSeasonNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Season>>>(SharedErrors.UnknownError);
        }
    }
}