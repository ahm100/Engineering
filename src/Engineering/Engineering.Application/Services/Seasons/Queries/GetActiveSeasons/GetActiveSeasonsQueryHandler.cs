
using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetActiveSeasons;

public class GetActiveSeasonsQueryHandler : IQueryHandler<GetActiveSeasonsQuery, DataResult<List<Season>>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetActiveSeasonsQuery> _logger;

    public GetActiveSeasonsQueryHandler(ILogger<GetActiveSeasonsQuery> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Season>>?>> Handle(GetActiveSeasonsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveSeasons(request.FilterData, request.BranchId, request.SeasonCode, request.SeasonName, request.CompanyId, request.PageIndex, request.PageSize, ct);

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